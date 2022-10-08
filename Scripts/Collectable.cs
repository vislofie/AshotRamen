using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private CollectableItems _item;

    public CollectableItems AttachedItem => _item;
}
