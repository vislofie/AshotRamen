using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Health : MonoBehaviour
{
    public static int PlayerHealth 
    {
        get => _playerHealth;

        set 
        {
            if (value > 100)
                _playerHealth = 100;
            else if (value < 0)
                _playerHealth = 0;
            else
                _playerHealth = value;
        }
    }

    [SerializeField]
    private Text _text;

    private static int _playerHealth = 100;

    private List<Effect> _effects = new List<Effect>();

    public void AddEffect(Effect effect)
    {
        _effects.Add(effect);
    }

    private IEnumerator CalculateEffects(float sec)
    {
        while (true)
        {
            foreach (var item in _effects)
            {
                item?.EffectValue();
                item.Duration--;
            }

            yield return new WaitForSeconds(sec);
        }
    }

    private void Start()
    {
        StartCoroutine(CalculateEffects(1.0f));
    }

    private void Update()
    {
        _text.text = PlayerHealth.ToString();

        for (int i = 0; i < _effects.Count; i++)
        {
            if (_effects[i].Duration <= 0)
            {
                _effects.Remove(_effects[i]);
            }
        }
    }

}
