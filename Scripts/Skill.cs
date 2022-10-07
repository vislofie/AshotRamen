using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public void DestroySkill()
    {
        this.enabled = false;
    }

    public void ApplySkill()
    {
        this.enabled = true;
    }
}
