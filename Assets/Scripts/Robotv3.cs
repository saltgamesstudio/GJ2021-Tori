using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Workshop;

public class Robotv3 : MonoBehaviour
{
    //this script let robot scan ground and wall for left/right turning decision. starting direction from transform z value
    private Rigidbody2D rigidbody2D;
    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform foot;
    [SerializeField] private Vector3 size;
    [SerializeField] private LayerMask maskSensor;
    [SerializeField] private bool isSensor;
    [SerializeField] private bool moveVertical;
    [SerializeField] private bool fly;


    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        if (fly)
        {
            rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private bool IsFacingTop()
    {
        return transform.localScale.y > Mathf.Epsilon;
    }

    private void FixedUpdate()
    {
        if (moveVertical)
        {
            if (IsFacingTop())
            {
                rigidbody2D.velocity = new Vector2(0f, speed);
            }
            else
            {
                rigidbody2D.velocity = new Vector2(0f, -speed);
            }
        }
        else
        {
            if (IsFacingRight())
            {
                rigidbody2D.velocity = new Vector2(speed, 0f);
            }
            else
            {
                rigidbody2D.velocity = new Vector2(-speed, 0f);
            }
        }
        

        isSensor = Physics2D.OverlapBox(foot.position, new Vector2(size.x, size.y), 0, maskSensor) != null ? true : false;
        if (isSensor)
        {
            if (moveVertical)
            {
                transform.localScale = new Vector2(transform.localScale.x, -(Mathf.Sign(rigidbody2D.velocity.y)));
                var sprite = GetComponent<SpriteRenderer>();
                //supaya sprite tetap berdiri
                if(sprite.flipY)
                {
                    sprite.flipY = false;

                }
                else
                {
                    sprite.flipY = true;
                }
            }
            else
            {
                transform.localScale = new Vector2(-(Mathf.Sign(rigidbody2D.velocity.x)), transform.localScale.y);
            }
            
        }

    }

    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        var player = otherCollider.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            if (player.yellowSerum == true)
            {
                Destroy(gameObject);
            }
            else
            {
                //TODO : Change to Proper UI Image
                player.Die();
            }
        }
    }
}
