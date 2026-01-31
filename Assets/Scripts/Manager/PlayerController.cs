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
        if (Mouse.current.leftButton.isPressed)
        {   
            Debug.Log("CLICK TRIGGERED");
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Set the NavMeshAgent's destination to the hit point
                agent.SetDestination(hit.point);
            }
        }
    }
}
