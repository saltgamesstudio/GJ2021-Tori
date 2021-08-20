using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Workshop;

public class Robot : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Vector2 moveDirection;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float minPatrol;
    [SerializeField] private float maxPatrol;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        moveDirection = new Vector2(Mathf.Lerp(minPatrol, maxPatrol, Mathf.PingPong(Time.time, 1)),0f);
        rigidbody2D.velocity = new Vector2(moveDirection.x * speed, rigidbody2D.velocity.y);
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
