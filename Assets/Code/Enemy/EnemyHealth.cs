using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[DisallowMultipleComponent]
public class EnemyHealth : Health
{
    Enemy _enemy;
   
    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }
    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
        _enemy.EnemyEffects.PlayOnGetDamage();
    }

    protected override void Die()
    {
        _enemy.IsAlive = false;
        _enemy._Player.PlayerQuests.CheckQuests();
        _enemy.Loot.DropLoot();
       // print("Die");
        Destroy(this.gameObject);
    }

    
}
