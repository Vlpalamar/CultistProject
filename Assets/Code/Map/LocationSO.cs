    using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Location_", menuName ="Scriptable Objects/Level/Location")]
public class LocationSO : ScriptableObject
{
    //[HideInInspector] public string guidId;

    #region HEADER Location
    [Header("Location")]
    [Space(10)]
    #endregion
    #region tooltip
    [Tooltip("Populate with locationPrefab")]
    #endregion
    public GameObject locationPrefab;

    #region HEADER List of Events
    [Header("List of Events")]
    [Space(10)]
    #endregion
    public List<EventSO> events;

    #region HEADER List of Events
    [Space(10)]
    [Header("List of bounds")]
    #endregion
    #region TOOLTIP
    [Tooltip("Populate with 2 coordinats for 1event  ")]
    #endregion
    public List<Vector2Int> areasOfEvents;


    //пока нет хаба, потом удалить 
    public Vector3 spawnPoint;
}
