using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int startLife = 10;

    public bool destroyOnKill = false;

    public float delayToKill = 0f;

    private int _currentLife;
    private bool _isDead;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _currentLife = startLife;
        _isDead = false;
    }

    public void Damage(int damage)
    {
        if (_isDead) return;

        _currentLife -= damage;

        if(_currentLife <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        _isDead = true;

        if (destroyOnKill)
        {
            Destroy(gameObject, delayToKill);
        }
    }
}