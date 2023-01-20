using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
        print("!!!!");
    }

    protected override void Die()
    {
        print("Die");
    }

    
}
