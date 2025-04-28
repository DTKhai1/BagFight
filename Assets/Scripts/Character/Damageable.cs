using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    private float _health;
    private float _maxHealth = 100f;
    public float Health
    {
        get { return _health; }
        set
        {
            _health = Mathf.Clamp(value, 0, _maxHealth);
            if (_health <= 0)
            {
                return;            // Handle death logic here
            }
        }
    }
    public float MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }
    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
}
