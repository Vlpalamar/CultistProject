using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings 
{

    #region ASTAR PATHFINDING PARAMS
    public const int defoultAStarMovementPenalty = 40;
    public const int preferredPathAStarMovementPenalty = 1;
    public const float playerMoveDistanceToRebuildPath = 5f;
    public const float enemyPathRebuildCooldown = 1f;
    public const int defoultAStarPenalty = 40;


    #endregion
}
