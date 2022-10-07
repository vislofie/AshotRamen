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
    }

    private void Update()
    {
        transform.Translate(_direction * Time.deltaTime * _speed);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.GetComponent<Brain>())
        {
            Destroy(gameObject);
        }
    }
}