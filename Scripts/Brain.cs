using UnityEngine;

public class Brain : MonoBehaviour
{
    [SerializeField]
    private Health _playerHealth;

    [SerializeField]
    private Collectable _collectableItem = null;

    private void Update()
    {
        FindItem();
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
