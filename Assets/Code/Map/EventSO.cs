using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event_", menuName = "Scriptable Objects/Level/Event")]
public class EventSO : ScriptableObject
{

    #region HEADER height and Width of Prefab

    [Header("populate with start position")]
    #endregion
    #region Tooltip
    [Tooltip("Take the coordinate brush in tile Pallet, check the  position of the higheast point of prefab  ")]
    #endregion
    public Vector2Int startCopyEventPosition;
    public Vector2Int startCopyEventFreePosition;


    #region HEADER height and Width of Prefab
    [Space(10)]
    [Header("height and Width of Prefab")]
    #endregion
    public Vector2Int proportions;


    #region HEADER LocationPrefab
    [Space(10)]
    [Header("LocationPrefab")]
    #endregion
    #region Tooltip
    [Tooltip("populate with prefab area with event")]
    #endregion
    public GameObject eventPrefab;

    #region Tooltip
    [Tooltip("populate with prefab area without event")]
    #endregion
    public GameObject eventFreePrefab;
}
