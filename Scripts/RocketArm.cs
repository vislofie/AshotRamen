using System.Collections;
using UnityEngine;

public class RocketArm : Skill
{
    private bool _isActive;

    [SerializeField]
    private GameObject _arm;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!_isActive)
            {
                _isActive = true;
                StartCoroutine(LaunchArm(10f));
            }
        }
    }

    private IEnumerator LaunchArm(float sec)
    {
        Instantiate(_arm, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(sec);
        _isActive = false;
    }


}
