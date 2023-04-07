using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{

    [SerializeField] protected HealthDetailsSO healthDetails;

    [SerializeField] protected float currentHealth;

    public float CurrentHealth { get => currentHealth; }
    public HealthDetailsSO HealthDetails { get => healthDetails; }

    public virtual void GetDamage(float damage)
    {
        print("HIT");
        if (currentHealth-damage>0)
            currentHealth = currentHealth - damage;
        else
        {
            currentHealth = 0;
            Die();
        }
    }

    protected virtual void Die()
    {
        print("You  Died");
    }


}
