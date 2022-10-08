using System.Collections;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private Vector2 _writeVector;

    public bool IsGrounded
    {
        get
        {
            return Physics2D.Raycast(transform.position, Vector2.down, 1.05f, _groundLayerMask);
        }
    }

    [SerializeField]
    private float _speed = 15.0f;
    [SerializeField]
    private float _jumpForce = 10.0f;
    [SerializeField]
    private float _dodgeForce = 10.0f;
    [SerializeField]
    private LayerMask _groundLayerMask;

    private bool _ableToMove;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _ableToMove = true;
    }

    public void TakeInputAndMove(Action onStartDodge, Action onEndDodge)
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (_ableToMove)
        {
            transform.Translate(new Vector2(horizontal * _speed * Time.deltaTime, 0));

            if (Input.GetKeyDown(KeyCode.Space))
                Jump();

            else if (Input.GetKeyDown(KeyCode.LeftAlt) && (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f))
                Dodge(horizontal, vertical, onStartDodge, onEndDodge);
        }
        
    }

    private void Jump()
    {
        if (IsGrounded)
            _rigidbody.AddForce(Vector2.up * _jumpForce);
    }

    private void Dodge(float horizontal, float vertical, Action onStartDodge, Action onEndDodge)
    {
        if (IsGrounded)
        {
            _ableToMove = false;
            
            StopAllCoroutines();

            Vector2 desiredPos = new Vector2(transform.position.x, transform.position.y) + new Vector2(horizontal, vertical).normalized * _dodgeForce;
            _writeVector = desiredPos;
            StartCoroutine(DodgeCoroutine(desiredPos, onStartDodge, onEndDodge));
        }
    }

    private IEnumerator DodgeCoroutine(Vector2 desiredPos, Action onStartDodge, Action onEndDodge)
    {
        float timer = 1.0f;
        _rigidbody.gravityScale = 0;

        onStartDodge.Invoke();

        while (timer >= 0.5f)
        {
            transform.position = Vector2.Lerp(new Vector2(transform.position.x, transform.position.y), desiredPos, timer * 10 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            timer -= Time.deltaTime;
        }

        _rigidbody.gravityScale = 2;
        _ableToMove = true;

        onEndDodge.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_writeVector, .3f);
    }
}
