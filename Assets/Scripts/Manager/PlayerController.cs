using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;

    public void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 moveVector = InputSystem.actions["Move"].ReadValue<Vector2>().normalized;
    }
}
