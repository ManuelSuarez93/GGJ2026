using System;
using UnityEngine;
using UnityEngine.Events;

public class TransitionZone : MonoBehaviour
{
    [SerializeField] private GameObject cinemachineCameraToActivate;
    [SerializeField] private GameObject cinemachineCameraToDeactivate;
    [SerializeField] private Transform movePlayerTo;
    [SerializeField] private UnityEvent onPlayerTransition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        GameManager.Instance.Player.transform.position = movePlayerTo.position;
        GameManager.Instance.Player.ActivateAgent(false);
        cinemachineCameraToActivate?.SetActive(true);
        cinemachineCameraToDeactivate?.SetActive(false);
        GameManager.Instance.Player.ActivateAgent(true);
        onPlayerTransition.Invoke();
        
    }
}
