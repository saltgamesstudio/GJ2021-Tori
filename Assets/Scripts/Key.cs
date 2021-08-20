using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Workshop;

public class Key : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D othercollider)
    {
        var player = othercollider.GetComponent<PlayerController>();
        if (player != null)
        {
            player.haveKey = true;
            Destroy(gameObject);
        }
        
    }
}
