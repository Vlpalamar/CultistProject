using System;
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
        _enemy.EnemyEffects.PlayOnGetDamage();
        base.GetDamage(damage);
       
    }

    protected override void Die()
    {
        _enemy.IsAlive = false;
       
       
        _enemy._Player.PlayerQuests.CheckQuests();
        _enemy.EnemyAnimation.SetDeathAnimation(_enemy.EnemyAnimation.CurrentDirection, _enemy.EnemyAnimation.AnimationDeath, false, 1);
       
        // print("Die");
    }

   

    protected void CheckQusts()
    {
        
    }
    public void KillHim()
    {
        Die();
    }

    public void AfterDeath()
    {
        _enemy.Loot.DropLoot();
        this.gameObject.SetActive(false);
    }
}
