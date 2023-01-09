using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{

    [SerializeField] protected HealthDetailsSO healthDetails;

    protected int currentHealth;

    public virtual void GetDamage(int damage)
    {
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

    }


}
