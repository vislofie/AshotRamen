using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIState { Idle, Patroling, ApproachingPlayer, Fighting, Dead }
public class EnemyBrain : MonoBehaviour
{
    private EnemyMovement _movement;
    private EnemySenses _senses;
    private EnemyAnimation _animation;

    private bool _beforeAttack;

    [SerializeField][Tooltip("Whether this bot is going to just stand and play some animation until player comes up or is it going to patrol\n" +
                             "If you put 'true' value, don't fill out PatrolPoints array")]
    private bool _chilling;
    [SerializeField]
    private Transform[] _patrolPoints;

    [SerializeField]
    private float _attackDistance = 0.5f;

    private AIState _state;

    private void Awake()
    {
        _state = AIState.Idle;

        _movement = GetComponent<EnemyMovement>();
        _senses = GetComponent<EnemySenses>();
        _animation = GetComponent<EnemyAnimation>();

        _beforeAttack = false;
    }

    private void Update()
    {
        if (_state != AIState.Dead)
        {
            _senses.FindVisibleTargets();

            if (_senses.ClosestVisibleTarget != null)
            {
                if (Vector2.Distance(_senses.ClosestVisibleTarget.position, transform.position) <= _attackDistance)
                {
                    _movement.AttackInGivenTime(_senses.ClosestVisibleTarget, 1.0f, BeforePrepareAttack, AfterPrepareAttack);
                }
                else
                {
                    _movement.MoveToTransform(_senses.ClosestVisibleTarget);
                }

            }

            if (_beforeAttack && _movement.TouchingPlayer)
            {
                PlayerBrain.Instance.ApplyDamage(10);
                _beforeAttack = false;
            }
        }

        
    }

    private void BeforePrepareAttack()
    {
        _beforeAttack = true;
    }

    private void AfterPrepareAttack()
    {
        _beforeAttack = false;
    }

    public void Die()
    {
        _state = AIState.Dead;
        _animation.ActivateDeathTrigger();
        _movement.StopAllCoroutines();

        Destroy(gameObject, 10.0f);
    }

}
