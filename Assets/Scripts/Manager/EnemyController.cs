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
    [SerializeField] private float timeToFindPlayer = 3f;
    [SerializeField] private FieldOfView fieldOfView;

    private EnemyState state;
    private EnemyState previousState;
    private float currentTimeToFindPlayer;
    enum EnemyState
    {
        Patrolling,
        Chasing,
        Looking
    }

    private void Start()
    {
        state = EnemyState.Patrolling;
    }

    private void Update()
    {
        if (fieldOfView.canSeePlayer)
        {
            ChangeState(EnemyState.Chasing);
            agent.SetDestination(fieldOfView.playerRef.transform.position);
        }
        else
        {
            if (state == EnemyState.Chasing && agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                ChangeState(EnemyState.Looking);
            }
            if (state == EnemyState.Looking )
            {
                if (currentTimeToFindPlayer < timeToFindPlayer)
                {
                    currentTimeToFindPlayer += Time.deltaTime;
                }
                else
                {
                    ChangeState(EnemyState.Patrolling);
                    currentTimeToFindPlayer = 0f;
                }
            }

            if (state == EnemyState.Patrolling)
            {
                if (agent.destination == transform.position)
                {
                    Vector3 insideUnitSphere = (Random.insideUnitSphere * fieldOfView.radius )+ transform.position;
                    Debug.Log($"Moving destination {insideUnitSphere}");
                    agent.SetDestination(insideUnitSphere);
                }
            }
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void ChangeState(EnemyState  newState)
    {
        previousState = state;
        state = newState;
    }
}