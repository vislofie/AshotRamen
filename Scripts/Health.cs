using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float PlayerHealth 
    {
        get => _playerHealth;

        set 
        {
            _playerHealth = Mathf.Clamp(value, 0, 100);
        }
    }

    private float _playerHealth = 100;
    private List<Effect> _effects = new List<Effect>();

    private void Start()
    {
        StartCoroutine(CalculateEffects(1.0f));
    }

    private IEnumerator CalculateEffects(float sec)
    {
        while (true)
        {
            for (int i = _effects.Count - 1; i >= 0; i--)
            {
                _effects[i].Duration = _effects[i].Duration - sec;
                PlayerHealth += _effects[i].Value;

                if (_effects[i].Duration <= 0)
                    _effects.Remove(_effects[i]);
            }

            yield return new WaitForSeconds(sec);
        }
    }

    /// <summary>
    /// Apply effect (buff, debuff) every second during the time
    /// </summary>
    /// <param name="hpRate">Change of HP every time interval</param>
    /// <param name="duration">Time of applying effect</param>
    public void AddEffect(float hpRate, float duration)
    {
        _effects.Add(new Effect(hpRate, duration));
    }
}
