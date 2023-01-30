using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CheckAPAth : MonoBehaviour
{
    bool isStart = false;
    [SerializeField] TileBase tileBase;


    Vector2Int startGridPosition = new Vector2Int(-1000, -1000);
    Vector2Int endGridPosition = new Vector2Int(-1000, -1000);
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!isStart)
                StartPath();

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            EndPath();

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Check();
        }
    }

    private void Check()
    {
        Vector3 vector = AimHelperUtilities.GetMouseWorldPosition();
        vector = AStar.Instance.Grid.WorldToCell(vector);
        print(AStar.Instance.AStarMovementPenaltyDictionary.Contains(new Vector2Int((int)vector.x, (int)vector.y)));
        print(AStar.Instance.Grid.WorldToCell(AimHelperUtilities.GetMouseWorldPosition()));
    }

    private void EndPath()
    {
        if (startGridPosition == new Vector2Int(-1000, -1000)) return;

        endGridPosition = (Vector2Int)AStar.Instance.Grid.WorldToCell(AimHelperUtilities.GetMouseWorldPosition());

        CreatePath(startGridPosition, endGridPosition); 
        startGridPosition = new Vector2Int(-1000, -1000);

    }

    private void CreatePath(Vector2Int startGridPosition, Vector2Int endGridPosition)
    {
        Queue<Vector2Int> vector2Ints= AStar.Instance.BuidlPath(startGridPosition, endGridPosition);
        do
        {
            Grid newg = AStar.Instance.Grid;
            newg.GetComponentsInChildren<Tilemap>()[0].SetTile((Vector3Int)vector2Ints.Dequeue(),tileBase );
          

        } while (vector2Ints.Count>0);

           
    }

    private void StartPath()
    {
         startGridPosition = (Vector2Int)AStar.Instance.Grid.WorldToCell(AimHelperUtilities.GetMouseWorldPosition());
        isStart = true;


    }
}
