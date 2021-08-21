using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerumBase : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] public float duration;
    [SerializeField] public float timeRemaining;
    [SerializeField] public bool timeIsRunning = false;

    [Header("Tween")]
    [SerializeField] private Vector3 tweenAmount;
    [SerializeField] private float tweenDuration;

    private void Start()
    {
        LeanTween.move(gameObject, transform.position + tweenAmount, tweenDuration).setLoopPingPong();
    }

    private void Awake()
    {
        timeRemaining = duration;
    }
}
