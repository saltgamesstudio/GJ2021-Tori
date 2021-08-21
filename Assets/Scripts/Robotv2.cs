using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Workshop;

public class Robotv2 : MonoBehaviour
{
    //this script let robot scan ground and wall for left/right turning decision. starting direction from transform z value
    private Rigidbody2D rigidbody2D;
    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform foot;
    [SerializeField] private Transform hand;
    [SerializeField] private Vector3 size;
    [SerializeField] private LayerMask maskGround;
    [SerializeField] private LayerMask maskDinding;
    [SerializeField] private bool noGround;
    [SerializeField] private bool noWall;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void FixedUpdate()
    {
        if(IsFacingRight())
        {
            rigidbody2D.velocity = new Vector2(speed,0f);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(-speed, 0f);
        }

        noGround = Physics2D.OverlapBox(foot.position, new Vector2(size.x, size.y), 0, maskGround) == null ? true : false;
        noWall = Physics2D.OverlapBox(hand.position, new Vector2(size.x, size.y), 0, maskDinding) == null ? true : false;
        if (!(noGround^noWall))
        {
            transform.localScale = new Vector2(-(Mathf.Sign(rigidbody2D.velocity.x)), transform.localScale.y);
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
                Debug.Log("You're ded");
            }
        }
    }
}
