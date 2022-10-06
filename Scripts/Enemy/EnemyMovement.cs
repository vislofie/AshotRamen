using System.Collections;
using UnityEngine;
using System;


public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed;

    private Coroutine _attackCoroutine;

    private Collider2D _collider;

    private bool _touchingPlayer;
    public bool TouchingPlayer => _touchingPlayer;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    public void MoveToTransform(Transform target)
    {
        transform.Translate((target.position - transform.position).normalized * _movementSpeed * Time.deltaTime);
    }

    public void MoveToPosition(Vector2 pos)
    {

    }

    public void AttackInGivenTime(Transform objToAttack, float time, Action onBeforeAttack, Action onAfterAttack)
    {
        if (_attackCoroutine == null)
        {
            _attackCoroutine = StartCoroutine(AttackCoroutine(objToAttack, time, onBeforeAttack, onAfterAttack));
        }
    }

    private IEnumerator AttackCoroutine(Transform objToAttack, float time, Action onBeforeAttack, Action onAfterAttack)
    {
        onBeforeAttack.Invoke();
        float timer = 0.0f;
        Vector2 ourPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 objPos = new Vector2(objToAttack.position.x, objToAttack.position.y);
        Vector2 desiredPos = ourPos + (ourPos - objPos).normalized;
        while (true)
        {
            transform.position = Vector2.Lerp(transform.position, desiredPos, 5 * Time.deltaTime);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            if (timer >= 1.0f) break;
        }

        timer = 0.0f;
        ourPos = new Vector2(transform.position.x, transform.position.y);
        objPos = new Vector2(objToAttack.position.x, objToAttack.position.y);
        desiredPos = ourPos + (objPos - ourPos).normalized * UnityEngine.Random.Range(8, 10);

        while (true)
        {
            transform.position = Vector2.Lerp(transform.position, desiredPos, 3 * Time.deltaTime);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            if (timer >= 0.6f) break;
        }

        onAfterAttack.Invoke();

        yield return new WaitForSeconds(0.5f);

        _attackCoroutine = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _touchingPlayer = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _touchingPlayer = false;
    }


}
