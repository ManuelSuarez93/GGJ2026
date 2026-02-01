using System;
using System.Collections;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    private void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck2D();
        }
    }

    private void FieldOfViewCheck2D()
    {
        Collider2D[] rangeChecks = Physics2D.OverlapCircleAll(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;

            Vector2 origin = transform.position;
            Vector2 targetPos = target.position;

            Vector2 directionToTarget = (targetPos - origin).normalized;

            // In 2D, "forward" is typically transform.up (top-down) or transform.right (side-scroller).
            // This assumes top-down (sprite facing up).
            if (Vector2.Angle(transform.up, directionToTarget) < angle / 2f)
            {
                float distanceToTarget = Vector2.Distance(origin, targetPos);

                RaycastHit2D hit = Physics2D.Raycast(origin, directionToTarget, distanceToTarget, obstructionMask);
                canSeePlayer = hit.collider == null;
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 viewAngle01 = DirectionFromAngle2D(transform.eulerAngles.z, -angle / 2f);
        Vector2 viewAngle02 = DirectionFromAngle2D(transform.eulerAngles.z, angle / 2f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(viewAngle01 * radius));
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(viewAngle02 * radius));
    }

    private Vector2 DirectionFromAngle2D(float eulerZ, float angleInDegrees)
    {
        angleInDegrees += eulerZ;
        float rad = angleInDegrees * Mathf.Deg2Rad;

        // 2D plane (XY): (cos, sin)
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }
}