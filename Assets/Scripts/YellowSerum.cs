using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Workshop;

public class YellowSerum : SerumBase
{
    private PlayerController player;
    private Sprite defaultskin;
    [HideInInspector] public float defaultMass;
    [HideInInspector] public float defaultSpeed;
    private Rigidbody2D rigidbody;
    [HideInInspector] public float defaultJumpVelocity;
    


    [SerializeField] private float playerMass;
    [SerializeField] private float debuffJump;
    [SerializeField] private float debuffSpeed;


    [Header("Colors")]
    [SerializeField] private Color primaryColor;
    [SerializeField] private Color combiWithRed;
    [SerializeField] private Color combiWithBlue;
    private Color defaultColor;

   

    private void OnTriggerEnter2D(Collider2D othercollider)
    {
        defaultskin = GetComponent<SpriteRenderer>().sprite;
        player = othercollider.GetComponent<PlayerController>();
        if (player!=null)
        {
            rigidbody = player.GetComponent<Rigidbody2D>();
            //amplify mass
            defaultMass = rigidbody.mass;
            rigidbody.mass = playerMass;
            //debuff jump
            defaultJumpVelocity = player.jumpVelocity;
            player.jumpVelocity = debuffJump;
            //debuff movement speed
            defaultSpeed = player.speed;
            player.speed = debuffSpeed;

            if (player.activeSerum.Count > 0)
            {
                foreach (var serum in player.activeSerum)
                {
                    serum.timeRemaining = this.duration;
                    if (serum is RedSerum)
                    {
                        player.jumpVelocity = (serum as RedSerum).defaultjumpVelocity;
                        defaultJumpVelocity = (serum as RedSerum).defaultjumpVelocity;
                    }
                    if (serum is BlueSerum)
                    {
                        player.jumpVelocity = defaultJumpVelocity;
                        player.speed = defaultSpeed;
                    }
                }
            }
            player.activeSerum.Add(this);
            player.yellowSerum = true;
            //despawn item
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            gameObject.GetComponent<Collider2D>().enabled = false;

            defaultColor = player.defaultColor;
            player.ChangeBraceletColor(primaryColor);
            if (player.redSerum)
            {
                player.ChangeBraceletColor(combiWithRed);
            }
            if (player.blueSerum)
            {
                player.ChangeBraceletColor(combiWithBlue);
            }
            
            timeIsRunning = true;
        }
        
    }

    private void Update()
    {
        //run respawn timer after picked up
        if (timeIsRunning)
        {
            if (timeRemaining > 0f)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0f;
                timeIsRunning = false;
                timeRemaining = 10f;
                rigidbody.mass = defaultMass;
                player.jumpVelocity = defaultJumpVelocity;
                player.speed = defaultSpeed;

                //respawn item
                gameObject.GetComponent<SpriteRenderer>().sprite = defaultskin;
                gameObject.GetComponent<Collider2D>().enabled = true;
                player.yellowSerum = false;
                player.activeSerum.Remove(this);
                player.ChangeBraceletColor(defaultColor) ;
            }
        }
    }
}
