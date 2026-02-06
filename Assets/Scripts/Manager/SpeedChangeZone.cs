using System;
using UnityEngine;

public class SpeedChangeZone : MonoBehaviour
{
    [SerializeField] private float speedChange = 8f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.Player.ChangeSpeed(speedChange);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.Player.ChangeSpeed(GameManager.Instance.Player.NormalSpeed);
        }
    }
}
