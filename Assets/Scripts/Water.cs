using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Workshop;

public class Water : MonoBehaviour
{

    [SerializeField] private float density;
    private BuoyancyEffector2D air;
    private PlayerController player;

    private void Awake()
    {
        air = GetComponent<BuoyancyEffector2D>();
        density = air.density;
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        player = otherCollider.GetComponent<PlayerController>();
        player.inWater = true;
        if (player.blueSerum)
        {
            air.density = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        player = otherCollider.GetComponent<PlayerController>();
        player.inWater = false;
        air.density = density;
    }


}
