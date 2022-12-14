using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChars : MonoBehaviour
{
    private float _hp;
    public float HP
    {
        get { return _hp; }
        set
        {
            _hp = Mathf.Clamp(value, 0, _maxHP);
        }
    }

    private float _maxHP;
    public float MaxHP => _maxHP;

    private List<Effect> _effects = new List<Effect>();

    private void Start()
    {
        _maxHP = 100;
        _hp = _maxHP;

        StartCoroutine(CalculateEffects(1.0f));
    }

    private IEnumerator CalculateEffects(float sec)
    {
        while (true)
        {
            for (int i = _effects.Count - 1; i >= 0; i--)
            {
                _effects[i].Duration = _effects[i].Duration - sec;
                HP += _effects[i].Value;

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
