using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Workshop;

public class DrownArea : MonoBehaviour
{
    private PlayerController player;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        player = otherCollider.GetComponent<PlayerController>();
        if (player != null)
        {
            if (player.blueSerum == true)
            {
                player.isDrowning = false;
                return;
            }
            player.isDrowning = true;
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        player = otherCollider.GetComponent<PlayerController>();
        if (player != null)
        {
            player.isDrowning = false;
        }
    }
}
