using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public void DestroySkill()
    {
        GetComponent<Skill>().enabled = false;
    }

    public void ApplySkill()
    {
        GetComponent<Skill>().enabled = true;
    }
}
