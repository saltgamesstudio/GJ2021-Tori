using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerumBase : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] public float duration = 10f;
    [SerializeField] public float timeRemaining = 10f;
    [SerializeField] public bool timeIsRunning = false;
}
