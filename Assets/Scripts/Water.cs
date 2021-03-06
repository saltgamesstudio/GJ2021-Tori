using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Workshop;

public class Water : MonoBehaviour
{
    private PlayerController player;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        player = otherCollider.GetComponent<PlayerController>();
        if(player != null)
        {
            player.inWater = true;
        }
        
    }


    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        player = otherCollider.GetComponent<PlayerController>();
        player.inWater = false;
    }


}
