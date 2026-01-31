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
        if (InputSystem.actions["Click"].triggered)
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.value), out hit, 3000))
            {
                agent.destination = hit.point;
            }
        }
    }
}
