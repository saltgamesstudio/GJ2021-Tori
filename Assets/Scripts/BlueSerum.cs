using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Workshop;

public class BlueSerum : SerumBase
{
    private PlayerController player;
    private Sprite defaultskin;
    public float defaultMass;
    private Rigidbody2D rigidbody;

    [SerializeField] private float playerMass;
    
    [Header("Colors")]
    [SerializeField] private Color primaryColor;
    [SerializeField] private Color combiWithYellow;
    [SerializeField] private Color combiWithRed;
    private Color defaultColor;

    private void OnTriggerEnter2D(Collider2D othercollider)
    {
        player = othercollider.GetComponent<PlayerController>();
        defaultskin = GetComponent<SpriteRenderer>().sprite;
        if (player!=null)
        {
            rigidbody = player.GetComponent<Rigidbody2D>();
            player.blueSerum = true;
            player.isDrowning = false;
            //amplify mass
            defaultMass = rigidbody.mass;
            rigidbody.mass = playerMass;

            if (player.activeSerum.Count > 0)
            {
                foreach (var serum in player.activeSerum)
                {
                    serum.timeRemaining = this.duration;
                    //kalau sebelumnya sudha ada yellow serum maka jump velocity menjadi normal, speed jadi normal
                    if (serum is YellowSerum)
                    {
                        player.jumpVelocity = (serum as YellowSerum).defaultJumpVelocity;
                        player.speed = (serum as YellowSerum).defaultSpeed;
                        //rigidbody.mass = (serum as YellowSerum).defaultMass;
                    }
                    //kalau sebelumnya sudah ada redserum maka mass reset
                    if (serum is RedSerum)
                    {
                        rigidbody.mass = defaultMass;
                    }
                }
            }
            player.activeSerum.Add(this);
            //despawn item
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            gameObject.GetComponent<Collider2D>().enabled = false;

            defaultColor = player.defaultColor;
            player.ChangeBraceletColor(primaryColor);
            if (player.redSerum)
            {
                player.ChangeBraceletColor(combiWithRed);
            }
            if (player.yellowSerum)
            {
                player.ChangeBraceletColor(combiWithYellow);
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
                //respawn item
                gameObject.GetComponent<SpriteRenderer>().sprite = defaultskin;
                gameObject.GetComponent<Collider2D>().enabled = true;
                player.blueSerum = false;
                rigidbody.mass = defaultMass;
                player.activeSerum.Remove(this);
                player.ChangeBraceletColor(defaultColor);
            }
        }
    }
}
