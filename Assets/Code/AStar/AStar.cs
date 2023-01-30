using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public  class AStar  :SingletonMonoBehaviour<AStar>
{
    private List<Node> _openList = new List<Node>();
    private Node _activeNode;
    private bool _isOver = false;

    private Queue<Vector2Int> readyStack = new Queue<Vector2Int>();
    private Grid grid;
    private List<Tilemap> obsteclLayers= new List<Tilemap>();
    private List<Vector2Int > aStarMovementPenaltyDictionary = new List<Vector2Int>() ;



    public Queue<Vector2Int> ReadyStack { get => readyStack;  }
    public Grid Grid { get
        {
            if (grid == null)
                grid = GameObject.Find("Grid").GetComponent<Grid>();
            return grid;
        }
            
    }

    public List<Vector2Int> AStarMovementPenaltyDictionary { get => aStarMovementPenaltyDictionary;  }

    //public AStar()
    //{
    //    readyQueue = new Queue<Vector2Int>();
    //}



    public Queue<Vector2Int> BuidlPath(Vector2Int startGridPosition, Vector2Int endGridPosition)
    {

        if (obsteclLayers.Count == 0)
            AddObstacleLayers();
        

        //поставить точку
        _activeNode = new Node(startGridPosition);
        _isOver = false;

        readyStack.Clear();
        int i = 0;
        do
        {
            i++;
            //посчитать до конца
            _activeNode.HCost = GetDistance(_activeNode.GridPosition, endGridPosition);

            //посчитать от начала 
            _activeNode.GCost = GetDistance(startGridPosition, _activeNode.GridPosition);

            //добавить всех соседей   //посчитать  стоимость соседей отдельно
            AddAllNeighbours(_activeNode, endGridPosition);

            // вібрать нужного 
            _activeNode = ChooseTheRightOne(endGridPosition);

            //добавить в стак 
            readyStack.Enqueue(_activeNode.GridPosition);


        } while (!_isOver && i <100) ;
        Debug.Log(i);
        Debug.Log("Done");
        return readyStack;
    }

    //private bool PlayerStayInObstaicleCell()
    //{
    //    Node playerNode = new Node(new Vector2Int(playerNode.GridPosition.x, playerNode.GridPosition.y));
    //}

    private void AddObstacleLayers()
    {
        obsteclLayers = new List<Tilemap>();
        TilemapCollider2D[] tilemapColliders = GameObject.Find("---Managers").gameObject.GetComponentsInChildren<TilemapCollider2D>();
        foreach (TilemapCollider2D item in tilemapColliders)
        {
            obsteclLayers.Add(item.GetComponent<Tilemap>());
        };

        AddObstacles();
    }

    
        
    

    private Node ChooseTheRightOne(Vector2Int endGridPosition)
    {
        if (_openList.Count == 0)
        {
            Debug.Log("List is empty");
            return _activeNode;
        }

        _openList.Sort();
       
        Node newNode= _openList[0];

        if (newNode.GridPosition==endGridPosition)
            _isOver = true;

        _openList.Clear();

        return newNode;
    }

    private void AddAllNeighbours(Node currentNode, Vector2Int endGridPosition)
    {
        
        for (int x = currentNode.GridPosition.x - 1, i= -1 ; i < 2; x++,i++)
        {
            for (int y = currentNode.GridPosition.y - 1, j=-1; j < 2; y++, j++)
            {
                if (i == 0 && j==0)
                    continue;

                Node node = new Node(new Vector2Int(x, y));
                if (CheckNodeIsObstacle(node))
                    continue;

                if (AlreadyInList(node))
                    continue;

                node.HCost = GetDistance(node.GridPosition, endGridPosition);
                node.GCost = GetDistance(currentNode.GridPosition, node.GridPosition)+currentNode.GCost ;
                node.ParentNode = currentNode;

                    _openList.Add(node);
            }
        }
    }

    private bool AlreadyInList(Node node)
    {
        if (readyStack.Contains(node.GridPosition))
            return true;
        else
            return false;
    }

    public bool CheckNodeIsObstacle(Node node)
    {
        if (aStarMovementPenaltyDictionary.Contains(node.GridPosition))
            return true;
        else 
            return false;
    }

    int GetDistance(Vector2Int nodeA, Vector2Int nodeB)
    {
        int dstX = Mathf.Abs(nodeA.x - nodeB.x);
        int dstY = Mathf.Abs(nodeA.y - nodeB.y);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }


    private void AddObstacles()
    {
        foreach (var tilemap in obsteclLayers)
        {
            foreach (var pos in tilemap.cellBounds.allPositionsWithin)
            {
                Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
                if (tilemap.HasTile(localPlace))
                {
                    aStarMovementPenaltyDictionary.Add((Vector2Int)localPlace);
                }
            }
        }
    }
}
