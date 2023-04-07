using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventArea : MonoBehaviour
{
    private string eventName;
    [SerializeField] private bool isActive;

    #region EnemiesInAreaDetails
    [Space(5)]
    [Header("EnemiesDetails")]
    #endregion
    #region Tooltip
    [Tooltip("Add Enemies, that u want to  spawn here in list ")]
    #endregion
    [SerializeField] protected List<EnemyDetailsSO> enemyPool = new List<EnemyDetailsSO>();

    #region Tooltip
    [Tooltip("Add enemy spawn points in List")]
    #endregion
    [SerializeField] protected List<Transform> EnemySpawnPoints = new List<Transform>();

    public string EventName { get => eventName; set => eventName = value; }
    public bool IsActive { get => isActive;  }

    public abstract void CompleteEvent();
   
}
