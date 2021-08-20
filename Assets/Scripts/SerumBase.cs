using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerumBase : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] public float duration;
    [SerializeField] public float timeRemaining;
    [SerializeField] public bool timeIsRunning = false;

    private void Awake()
    {
        timeRemaining = duration;
    }
}
