using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings 
{

    public const KeyCode KeyCode_Use = KeyCode.E;



    #region AUDIO
    public const float musicFadeOutTime = 0.5f;
    public const float musicFadeInTime = 0.5f;
    #endregion

    #region ASTAR PATHFINDING PARAMS
    public const int defoultAStarMovementPenalty = 40;
    public const int preferredPathAStarMovementPenalty = 1;
    public const float playerMoveDistanceToRebuildPath = 6f;
    public const float enemyPathRebuildCooldown = 1f;
    public const int defoultAStarPenalty = 40;


    #endregion
}
