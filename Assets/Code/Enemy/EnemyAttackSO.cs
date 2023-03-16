using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyAttack_", menuName = "Scriptable Objects/Enemy/Attack")]
public class EnemyAttackSO : ScriptableObject  
{
    [SerializeField] private float damage;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackSpeed;

    public float Damage { get => damage;  }
    public float AttackRange { get => attackRange;  }
    public float AttackSpeed { get => attackSpeed;   }
}
