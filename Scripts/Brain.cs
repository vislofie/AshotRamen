using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    [SerializeField]
    private Health _playerHealth;

    [SerializeField]
    private Collectable _collectableItem = null;

    [SerializeField]
    private BackGroundMusic _music;

    private List<Skill> _skills = new();

    private void Start()
    {
        _skills.AddRange(gameObject.GetComponents<Skill>());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            FindItem();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for(int i = 0; i < _skills.Count; i++)
            {
                _skills[i].DestroySkill();
            }
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            for (int i = 0; i < _skills.Count; i++)
            {
                _skills[i].ApplySkill();
            }
        }
    }

    private void FindItem()
    {
        if (_collectableItem != null)
        {
            var hpRate = _collectableItem.AttachedItem;
            _playerHealth.AddEffect(hpRate.HpRange, hpRate.Duration);
            Destroy(_collectableItem.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collectable>())
        {
            _collectableItem = collision.GetComponent<Collectable>();
        }
    }
}
