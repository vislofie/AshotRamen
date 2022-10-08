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

    private void Start()
    {
        _maxHP = 100;
        _hp = _maxHP;
    }
}
