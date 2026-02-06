using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private EnemyBehaviour behaviour;

    [Header("Searching")]
    [SerializeField] private float timeToFindPlayer = 3f;
    [SerializeField] private DetectionZone detectionZone;
    [SerializeField] private float radiusPatrolZone = 5f;
    
    [Space]
    [SerializeField] private PlayerMask.MaskType enemyMaskType;

    [Header("Audio")]
    [SerializeField] private AudioSource enemyAudioSource;
    [SerializeField] private List<AudioClip> screamClips;
    [SerializeField] private float maxVolumeScream;
    [SerializeField] private SpriteRenderer enemyRenderer;
    [SerializeField] private float minVolumeScream;

    [Header("Attack")]
    [SerializeField] private float attackChargeTime;
    [SerializeField] private float minimumDistance;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color attackColor = Color.red;

    private EnemyState state;
    private EnemyState previousState;
    private float currentTimeToFindPlayer;
    private float currentAttackCharge;
    private Vector2 targetPosition; 
    enum EnemyState
    {
        Patrolling,
        Chasing,
        Looking
    }

    enum EnemyBehaviour
    {
        Follower,
        Stalker,
        Patrol
    }

    private void Start()
    {
        enemyAudioSource.volume = minVolumeScream;
        state = EnemyState.Patrolling;
    }

    private void Update()
    { 
        SetDestination();
        Attack();
    }

    private void FixedUpdate()
    {
        if (animator)
            animator.SetBool("IsWalking", agent.velocity.magnitude > 1f); 
        
        
        enemyRenderer.flipX = agent.velocity.x > 0;
    }


    #region Enemy Behaviour
    private void SetDestination()
    {
        switch (behaviour)
        {
            case EnemyBehaviour.Follower: break;
            case EnemyBehaviour.Patrol:  Patrol();; break;
            case EnemyBehaviour.Stalker: agent.SetDestination(PlayerPosition); break;
        }
    }

    private void Patrol()
    {
        if (detectionZone.IsDetected && PlayerHasDifferentMask())
        {
            ChangeState(EnemyState.Chasing);
            targetPosition = PlayerPosition; 
        }
        else
        {
            if (state == EnemyState.Chasing)
            {
                ChangeState(EnemyState.Patrolling); 
            }

            if (state == EnemyState.Patrolling)
            {
                if (!agent.hasPath)
                {
                    Vector2 randomPointInside = Random.insideUnitCircle * radiusPatrolZone;
 
                    Vector3 finalPosition = transform.position + new Vector3(randomPointInside.x, 0, randomPointInside.y);

                    agent.SetDestination(finalPosition);
                }
            }
        }
    }

    private void Follow()
    {
        if (detectionZone.IsDetected && PlayerHasDifferentMask())
        {
            ChangeState(EnemyState.Chasing);
            targetPosition = PlayerPosition; 
            agent.SetDestination(targetPosition);
            
        }
        else if(!detectionZone.IsDetected)
        {
            if (state == EnemyState.Chasing)
            {
                ChangeState(EnemyState.Looking);
                currentTimeToFindPlayer = 0f;
                agent.SetDestination(PlayerPosition);
            }

            if (state == EnemyState.Looking)
            {
                agent.SetDestination(targetPosition);
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
        }
    }
    
    #endregion

    private static Vector3 PlayerPosition => GameManager.Instance.Player.transform.position;

    private bool PlayerHasDifferentMask()
    {
        return enemyMaskType != GameManager.Instance.CurrentPlayerMask.CurrentType;
    }

    private void Attack()
    {
        if (currentAttackCharge <= attackChargeTime && detectionZone.IsDetected && PlayerHasDifferentMask())
        {
            currentAttackCharge += Time.deltaTime;

            ChangeAttackCircleAlpha(true);
            ChangeSoundAttack(maxVolumeScream);
        }
        else if (currentAttackCharge >= attackChargeTime && detectionZone.IsDetected)
        {
            DoAttack();
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0f);
            currentAttackCharge = 0f;

            ChangeAttackCircleAlpha(false);
            ChangeSoundAttack(minVolumeScream);
        }
        else if (!detectionZone.IsDetected)
        {
            if (currentAttackCharge > 0f)
                currentAttackCharge -= Time.deltaTime;

            ChangeSoundAttack(minVolumeScream);
            ChangeAttackCircleAlpha(false);
        }
    }

    private void ChangeAttackCircleAlpha(bool attack)
    {
        if (true)
        {
            renderer.color = Color.Lerp(normalColor, attackColor, currentAttackCharge / attackChargeTime);
        }
        else
        { 
            renderer.color = Color.Lerp(attackColor, normalColor, currentAttackCharge / attackChargeTime);
        }
    }

    private void ChangeSoundAttack(float volumeTo)
    {
        if (!enemyAudioSource.isPlaying && detectionZone.IsDetected)
        {
            enemyAudioSource.clip = screamClips[Random.Range(0, screamClips.Count - 1)];
            enemyAudioSource.Play();
        }
        float startValue = enemyAudioSource.volume;
        float newValue = Mathf.Lerp(startValue, volumeTo, currentAttackCharge / attackChargeTime);
        enemyAudioSource.volume = newValue;
    }

    private void DoAttack()
    {
        if (detectionZone.IsDetected)
        {
            GameManager.Instance.KillPlayer();
        }
    }

    private void ChangeState(EnemyState newState)
    {
        previousState = state;
        state = newState;
    }
}