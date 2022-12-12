using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrentPlayer_", menuName = "Scriptable Objects/Player/CurrentPlayer")]
public class CurrentPlayerSO : ScriptableObject
{
    [SerializeField] private string playerName;
    [SerializeField] private PlayerDetailsSO playerDetails;

    public string PlayerName1 { get => playerName; }
    public PlayerDetailsSO PlayerDetails { get => playerDetails;  }
}
