using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Damageable
{
    public float MaxHealth { get; }
    public float Health { get; set; }
    public void TakeDamage(float damage) { }
}
