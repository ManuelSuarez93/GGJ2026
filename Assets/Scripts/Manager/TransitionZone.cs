using System;
using UnityEngine;

public class TransitionZone : MonoBehaviour
{
    [SerializeField] private GameObject cinemachineCameraToActivate;
    [SerializeField] private GameObject cinemachineCameraToDeactivate;
    [SerializeField] private Transform movePlayerTo;

    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.Player.transform.position = movePlayerTo.position;
        GameManager.Instance.Player.ActivateAgent(false);
        cinemachineCameraToActivate.SetActive(true);
        cinemachineCameraToDeactivate.SetActive(false);
        GameManager.Instance.Player.ActivateAgent(true);
        
    }
}
