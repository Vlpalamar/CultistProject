using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Enemy))]
[DisallowMultipleComponent]
public abstract class EnemyAttack : MonoBehaviour
{
    [SerializeField] private EnemyAttackSO attackDetails;

    protected Player _player;
    protected Enemy _enemy;
    virtual protected   void  Start()
    {
        _enemy = GetComponent<Enemy>();
        _player = GameManager.Instance.GetPlayer() ;
    }


    public EnemyAttackSO AttackDetails { get => attackDetails;  }

    public  abstract void StartAttack();

    public abstract void DealDamage(float Damage);
}
