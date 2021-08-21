using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Workshop;

public class RedSerum : SerumBase
{
    [SerializeField] private float amplifier = 1.8f;
    private float defaultjumpVelocity;
    private PlayerController player;
    private Sprite defaultskin;
            
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
            player.redSerum = true;

            if(player.activeSerum.Count > 0)
            {
                foreach(var serum in player.activeSerum)
                {
                    serum.timeRemaining = this.duration;
                }
            }
            player.activeSerum.Add(this);

            //enhance jump
            defaultjumpVelocity = player.jumpVelocity;
            player.jumpVelocity = player.jumpVelocity * amplifier;

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
                //reset jump
                player.jumpVelocity = defaultjumpVelocity;
                //respawn item
                gameObject.GetComponent<SpriteRenderer>().sprite = defaultskin;
                gameObject.GetComponent<Collider2D>().enabled = true;
                player.redSerum = false;
                player.activeSerum.Remove(this);
                player.ChangeBraceletColor(defaultColor);
            }
        }
    }

}
