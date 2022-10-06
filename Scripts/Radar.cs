using System.Collections;
using UnityEngine;

public class Radar : Skill
{
    private bool _isActive;

    [SerializeField]
    private float _radius = 4f;

    [SerializeField]
    private LayerMask _highlighted;

    void Start()
    {
        StartCoroutine(CoolDown(1f));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _isActive = true;
        }
    }

    private IEnumerator CoolDown(float sec)
    {
        while (true)
        {
            if (_isActive)
            {
                Collider2D[]? isHighLight = ColliderInRadius(Time.deltaTime * 5);
                Outline outline;

                if (isHighLight != null)
                {
                    for(int i = isHighLight.Length - 1; i >=0; i--)
                    {
                        outline = isHighLight[i].gameObject.AddComponent<Outline>();
                        outline.OutlineColor = Color.yellow;
                        outline.OutlineWidth = 5f;
                    }
                    yield return new WaitForSeconds(10f);
                    for(int i = isHighLight.Length - 1; i >= 0; i--)
                    {
                        if (isHighLight[i] != null)
                            Destroy(isHighLight[i].GetComponent<Outline>());
                    }
                    _isActive = false;
                }
            }
            yield return new WaitForSeconds(sec);
        }
    }

    private Collider2D[] ColliderInRadius(float time)
    {
        Collider2D[]? collider = Physics2D.OverlapCircleAll(transform.position, _radius, _highlighted);
        if (collider.Length <= 0)
        {
            collider = null;
            _isActive = false;
        }
        return collider;
    }

}
