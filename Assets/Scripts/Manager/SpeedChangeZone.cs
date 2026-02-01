using System;
using UnityEngine;

public class SpeedChangeZone : MonoBehaviour
{
    [SerializeField] private float speedChange = 8f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.Player.ChangeSpeed(speedChange);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.Player.ChangeSpeed(GameManager.Instance.Player.NormalSpeed);
        }
    }
}
