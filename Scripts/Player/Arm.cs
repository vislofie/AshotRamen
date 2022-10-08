using System.Collections;
using UnityEngine;

public class Arm : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private Vector3 _direction;

    private void Start()
    {
        var dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _direction = new Vector3(dir.x, dir.y, 0).normalized;

        transform.right = _direction;

        Destroy(gameObject, 15.0f);
    }

    private void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y) + new Vector2(_direction.x, _direction.y) * Time.deltaTime * _speed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log(collision.collider.name);

        if (!collision.gameObject.GetComponent<PlayerBrain>())
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.GetComponent<PlayerBrain>())
        {
            if (collision.gameObject.GetComponent<EnemyBrain>())
            {
                collision.gameObject.GetComponent<EnemyBrain>().Die();
            }

            Destroy(gameObject);
        }
    }
}