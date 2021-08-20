using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Workshop
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D rigidbody2D;
        private Vector2 moveDirection;
        [SerializeField] private float speed = 0.1f;

        [Header("Foot")]
        [SerializeField] private Transform foot;
        [SerializeField] private Transform leftfoot;
        [SerializeField] private Transform rightfoot;
        [SerializeField] private float radius;
        [SerializeField] private Vector3 size;
        [SerializeField] private bool isGrounded;
        [SerializeField] private LayerMask mask;
        [SerializeField] private LayerMask maskDinding;
        [SerializeField] private bool leftTouched;
        [SerializeField] private bool rightTouched;

        [Header("Jump")]
        [SerializeField] public float jumpVelocity = 12f;
        [SerializeField] public float fallMultiplier = 2.5f;
        [SerializeField] public float lowJumpMultiplier = 2f;
        [SerializeField] public float heightTreshold = -18f;

        [Header("Key")]
        [SerializeField] public bool haveKey = false;

        [Header("Swim")]
        [SerializeField] public bool inWater = false;
        [SerializeField] public float inWaterTreshold = 5f; //berlaku untuk oksigen dan air, harusnya bukan inWaterTreshold melainkan isDrowningTreshold
        private float defaultInWaterTreshold; //harusnya defaultIsDrowningTreshold
        private float defaultMass;
        [SerializeField] public bool isDrowning = false;


        [Header("SuperPower")]
        [SerializeField] public bool redSerum = false;
        [SerializeField] public bool blueSerum = false;
        [SerializeField] public bool yellowSerum = false;
        [SerializeField] public List<SerumBase> activeSerum;

        [Header("Animation")]
        private Animator animator;
        private SpriteRenderer spriteRenderer;
        private static readonly int ANIM_VelocityY = Animator.StringToHash("velocityY");
        private static readonly int ANIM_IsMovingX = Animator.StringToHash("isMovingX");
        private static readonly int ANIM_IsGround = Animator.StringToHash("isGround");
        private static readonly int ANIM_jump = Animator.StringToHash("jump");
        private static readonly int ANIM_inWater = Animator.StringToHash("inWater");
        private static readonly int ANIM_isDrowning = Animator.StringToHash("isDrowning");

        [Header("Collider")]
        [SerializeField] private Collider2D groundCollider;
        [SerializeField] private Collider2D waterCollider;



        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            defaultInWaterTreshold = inWaterTreshold;
            defaultMass = rigidbody2D.mass;
        }

        private void Update()
        {
            leftTouched = Physics2D.OverlapBox(leftfoot.position, new Vector2(size.x, size.y), 0, maskDinding) != null ? true : false;
            rightTouched = Physics2D.OverlapBox(rightfoot.position, new Vector2(size.x, size.y), 0, maskDinding) != null ? true : false;

            //left & right walk direction
            if (Input.GetKeyUp(KeyCode.A))
            {
                moveDirection = new Vector2(0f, 0f);
                animator.SetBool(ANIM_IsMovingX, false);
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                moveDirection = new Vector2(0f, 0f);
                animator.SetBool(ANIM_IsMovingX, false);

            }

            //prevent stick to wall
            if (Input.GetKey(KeyCode.A) && !leftTouched)
            {
                moveDirection = new Vector2(-1f, 0f);
                animator.SetBool(ANIM_IsMovingX, true);
                spriteRenderer.flipX = true;
            }
            

            if (Input.GetKey(KeyCode.D) && !rightTouched)
            {
                moveDirection = new Vector2(1f, 0f);
                animator.SetBool(ANIM_IsMovingX, true);
                spriteRenderer.flipX = false;
            }

            

            //jump function
            isGrounded = Physics2D.OverlapCircle(foot.position, radius, mask) != null ? true : false;
            animator.SetBool(ANIM_IsGround, isGrounded);

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rigidbody2D.velocity = Vector2.up * jumpVelocity;
                if (!yellowSerum)
                {
                    animator.SetTrigger(ANIM_jump);
                }
            }

            //betterJump function
            if(rigidbody2D.velocity.y < 0f)
            {
                rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if(rigidbody2D.velocity.y > 0f && Input.GetKeyUp(KeyCode.Space))
            {
                rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
            animator.SetFloat(ANIM_VelocityY, rigidbody2D.velocity.y);
            animator.SetBool(ANIM_inWater, inWater);

            //ded by overdose
            if (redSerum && blueSerum && yellowSerum)
            {
                Debug.Log("You're ded from overdose");
            }

            //death by fall
            if (rigidbody2D.velocity.y <= heightTreshold && isGrounded)
            {
                Debug.Log("ded by fall");
            }


            //timer
            if(blueSerum)
            {
                animator.SetBool(ANIM_isDrowning, !isDrowning);
                //death by suffocation
                if (!isDrowning)
                {
                    inWaterTreshold -= Time.deltaTime;
                    if (inWaterTreshold < 0f)
                    {
                        Debug.Log("ded by suffocation");
                    }
                }
                else
                {
                    inWaterTreshold = defaultInWaterTreshold;
                }
            }
            else
            {
                animator.SetBool(ANIM_isDrowning, isDrowning);
            }
            
            if(yellowSerum)
            {
                //death by drowning
                if (isDrowning)
                {
                    inWaterTreshold -= Time.deltaTime;
                    if (inWaterTreshold < 0f)
                    {
                        Debug.Log("ded by drowning");
                    }
                }
                else
                {
                    inWaterTreshold = defaultInWaterTreshold;
                }
            }

            
            

        }

        private void FixedUpdate()
        {
            //player movement speed in water
            if (inWater)
            {
                rigidbody2D.velocity = new Vector2(moveDirection.x * speed / 3, rigidbody2D.velocity.y);
                waterCollider.enabled = true;
                groundCollider.enabled = false;

            }
            else
            {
                //player movement speed on ground
                rigidbody2D.velocity = new Vector2(moveDirection.x * speed, rigidbody2D.velocity.y);
                waterCollider.enabled = false;
                groundCollider.enabled = true;
            }
            
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(foot.position, radius);
            Gizmos.DrawCube(leftfoot.position, size);
            Gizmos.DrawCube(rightfoot.position, size);
        }


    }

}


