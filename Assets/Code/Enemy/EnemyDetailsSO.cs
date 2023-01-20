using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDetails_", menuName = "Scriptable Objects/Enemy/Details")]
public class EnemyDetailsSO : ScriptableObject
{
    #region Header Base Enemmy Details
    [Space(10)]
    [Header("BaseEnemyDetails")]
    #endregion

    [SerializeField] private string enemyName;

    [SerializeField] private GameObject enemyPrefab;

    #region ToolTip
    [Tooltip("Distance to the player, before enemy start chasing")]
    #endregion
    [SerializeField] private float chaseDistance;

     
    


    public float ChaseDistance { get => chaseDistance;  }
    public string EnemyName { get => enemyName; }
    public GameObject EnemyPrefab { get => enemyPrefab;  }
}
