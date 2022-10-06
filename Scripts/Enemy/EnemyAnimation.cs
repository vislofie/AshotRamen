using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ActivateDeathTrigger()
    {
        _animator.enabled = true;
        _animator.SetTrigger("Dead");
    }
}
