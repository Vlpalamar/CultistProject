using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "GameResources_", menuName = "Scriptable Objects/GameResources")]
public  class GameResources : ScriptableObject
{
    #region PLAYER
    [Space(10)]
    [Header("Player")]
    #endregion
    #region
    [Tooltip("This is used for reference between sceens")]
    #endregion
    public PlayerDetailsSO currentPlayerDetails;


    #region  Sounds
    [Space(10)]
    [Header("Sounds")]
    #endregion
    public AudioMixerGroup audioMasterMixerGroup;



   
}

