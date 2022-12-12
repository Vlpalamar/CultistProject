using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerDetails_", menuName ="Scriptable Objects/Player/PlayerDetails")]
public class PlayerDetailsSO : ScriptableObject
{
    #region Header Player Details
    [Space(10)]
    [Header("Player Details")]
    #endregion
    [SerializeField] private string ñharacterName;

    [SerializeField] private GameObject playerPrefab;

    #region Header Player Details
    [Space(10)]
    [Header("Health")]
    #endregion
    [SerializeField] private int playerStartHealth;

    [SerializeField] private Sprite characterMiniMapSprite;     //ïîêà íåòó ïîòîì ìîæåò áóäåò 


    public string ÑharacterName { get => ñharacterName; }
    public GameObject PlayerPrefab { get => playerPrefab; }
    public int PlayerStartHealth { get => playerStartHealth;  }
    public Sprite CharacterMiniMapSprite { get => characterMiniMapSprite; }

}

