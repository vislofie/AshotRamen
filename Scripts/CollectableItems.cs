using UnityEngine;

[CreateAssetMenu(menuName = "CollectableItems", order = 0)]
public class CollectableItems : ScriptableObject
{
    public string Name;

    public float HpRange;

    public float Duration;
}
