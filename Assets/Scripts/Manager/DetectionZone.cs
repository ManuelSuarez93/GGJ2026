using System;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public bool IsDetected { get; private set; }
    private void OnTriggerEnter(Collider other)
    {
        IsDetected = other.CompareTag("Player");
    }

    private void OnTriggerExit(Collider other)
    {
        IsDetected = other.CompareTag("Player") && IsDetected ? false : IsDetected;
        ;
    }
}
