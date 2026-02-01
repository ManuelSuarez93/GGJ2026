using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float speed = 5f;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private LayerMask layerMask;

    [Header("Input System")]
    [SerializeField] private string clickMoveActionName = "ClickMove"; // create this in your Input Actions asset

    [Header("Audio")]
    [SerializeField] private AudioSource footsteps;
    [SerializeField] private float footstepRate = 0.5f;
    [SerializeField] private AudioSource effects;

    private float currentFootstepTime = 0f;
    public float NormalSpeed { get; private set; }

    private Vector2 targetPosition;
    private void Start()
    {
        NormalSpeed = speed;
    }


  

    public void FixedUpdate()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            Vector3 screenPosition = Mouse.current.position.value;

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            agent.SetDestination(worldPosition);
        }

        if (animator)
            animator.SetBool("IsWalking", agent.velocity.magnitude > 0.0001f);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pickup"))
        {
            GameManager.Instance.AddPhotoCollected();
            other.gameObject.SetActive(false);
        }
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
