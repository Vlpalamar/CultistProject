using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class GameResources : MonoBehaviour
{
    private static GameResources instance;

    public static GameResources Instance
    {
        get
        {
            if (instance==null)
            {
                instance = Resources.Load<GameResources>("ResourcesManager");
            }
            return instance;
        }
    }

    #region PLAYER
    [Space(10)]
    [Header("Player")]
    #endregion
    #region
    [Tooltip("This is used for reference between sceens")]
    #endregion
    [SerializeField] private CurrentPlayerSO currentPlayer;
    public CurrentPlayerSO CurrentPlayer{ get; }

}
