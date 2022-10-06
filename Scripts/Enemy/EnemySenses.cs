using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySenses : MonoBehaviour
{
    private List<Transform> _visibleTargets = new List<Transform>();
    public List<Transform> VisibleTargets => _visibleTargets;

    public Transform ClosestVisibleTarget
    {
        get
        {
            if (_visibleTargets.Count > 0)
            {
                float shortestDistance = Vector3.Distance(transform.position, _visibleTargets[0].position);
                Transform closestTarget = _visibleTargets[0];
                foreach (Transform target in _visibleTargets)
                {
                    float curDist = Vector3.Distance(transform.position, target.position);
                    if (curDist < shortestDistance)
                    {
                        shortestDistance = curDist;
                        closestTarget = target;
                    }
                }
                return closestTarget;
            }
            else
            {
                return null;
            }
        }
    }

    [SerializeField][Tooltip("Layers of entities that this entity should see")]
    private LayerMask _entitiesMask;
    [SerializeField][Tooltip("Layers of obstacles that this entity should not see through")]
    private LayerMask _obstaclesMask;
    [SerializeField]
    private float _normalViewRadius = 10;

    public void FindVisibleTargets()
    {
        _visibleTargets.Clear();
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, _normalViewRadius, _entitiesMask);

        foreach (Collider2D targetCollider in targetsInViewRadius)
        {
            Transform targetTransform = targetCollider.transform;
            Vector2 dirToTarget = targetTransform.position - transform.position;
            dirToTarget.Normalize();

            float distance = Vector3.Distance(transform.position, targetTransform.position);
            if (!Physics2D.Raycast(transform.position, dirToTarget, distance, _obstaclesMask))
            {
                _visibleTargets.Add(targetTransform);
            }
        }
    }
}
