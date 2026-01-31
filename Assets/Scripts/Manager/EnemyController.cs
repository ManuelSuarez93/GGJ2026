using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float radiusFindPlayer = 20f;
    [SerializeField] private float timeToSearchAgain = 0.5f;
    [SerializeField] private float timeToSearch = 1.5f;
    [SerializeField] private float radiusNewPosition;
    
    private void Update()
    {
        SearchPlayerRoutine();
    }

    IEnumerator SearchPlayerRoutine()
    {
        while (true)
        {
            Collider playersColliders = Physics.OverlapSphere(transform.position, radiusFindPlayer, 1 << LayerMask.NameToLayer("Player")).FirstOrDefault();
            if (playersColliders != null)
            {
                agent.SetDestination(playersColliders.transform.position);
            }
            else
            {
                if (agent.destination == transform.position)
                {
                    Vector3 insideUnitSphere = (Random.insideUnitSphere * radiusFindPlayer )+ transform.position;
                    Debug.Log($"Moving destination {insideUnitSphere}");
                    agent.SetDestination(insideUnitSphere);
                }
            }
        }
      
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f); 
        Gizmos.DrawWireSphere(transform.position, radiusFindPlayer);
        Gizmos.color = new Color(1f, 1f, 1f);
        Gizmos.DrawWireSphere(transform.position, radiusNewPosition);
    }
}
