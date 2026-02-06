using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float speed = 5f;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private SpriteRenderer renderer;

    [Header("Input System")]
    [SerializeField] private string clickMoveActionName = "ClickMove"; // create this in your Input Actions asset

    [Header("Audio")]
    [SerializeField] private AudioSource footsteps;
    [SerializeField] private List<AudioClip> footstepClips;
    [SerializeField] private float footstepRate = 0.5f;
    [SerializeField] private AudioSource effects;
    [SerializeField] private AudioClip pickupClip;

    private float currentFootstepTime = 0f;
    public float NormalSpeed { get; private set; }

    private Vector2 targetPosition;
    private void Start()
    {
        NormalSpeed = agent.speed;
    }
    

    public void FixedUpdate()
    {
        if (Mouse.current.leftButton.isPressed && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 screenPosition = Mouse.current.position.value;

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            agent.SetDestination(worldPosition);
        }

        if (animator)
            animator.SetBool("IsWalking", agent.velocity.magnitude > 0.0001f);
        if (agent.velocity.magnitude > 0.0001f)
            PerformFootstep();

        renderer.flipX = agent.velocity.x > 0;
    }

    public void PerformFootstep()
    {
        if (currentFootstepTime < footstepRate)
        {
            currentFootstepTime += Time.deltaTime;
        }
        else
        {
            footsteps.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
            footsteps.Play();
            currentFootstepTime = 0f;
        }
    }

    public void ChangeSpeed(float speed)
    {
        agent.speed = speed;
    }

    public void ActivateAgent(bool enabled)
    {
        agent.enabled = enabled;
    }
}
