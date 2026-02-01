using System;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public bool IsDetected { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) IsDetected = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player") && IsDetected) IsDetected = false;
    }
}
