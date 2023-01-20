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



    public Queue<Vector2Int> ReadyStack { get => readyStack;  }
    public Grid Grid { get
        {
            if (grid == null)
                grid = GameObject.Find("Grid").GetComponent<Grid>();
            return grid;
        }
            
    }

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
        
        do
        {
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


        } while (!_isOver );

        Debug.Log("Done");
        return readyStack;
    }

    private void AddObstacleLayers()
    {
        obsteclLayers = new List<Tilemap>();
        TilemapCollider2D[] tilemapColliders = GameObject.Find("---Managers").gameObject.GetComponentsInChildren<TilemapCollider2D>();
        foreach (TilemapCollider2D item in tilemapColliders)
        {
            obsteclLayers.Add(item.GetComponent<Tilemap>());
        };
        
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

                //проверка на то относятся ли они к валидной клетке 
                //print("populate");
                Node node = new Node(new Vector2Int(x, y));
                node.HCost = GetDistance(node.GridPosition, endGridPosition);
                node.GCost = GetDistance(currentNode.GridPosition, node.GridPosition)+currentNode.GCost ;
                node.ParentNode = currentNode;

                if(CheckNodeIsObstacle(node))
                    continue;

                    _openList.Add(node);

            }
        }
    }

    private bool CheckNodeIsObstacle(Node node)
    {
        foreach (Tilemap t in obsteclLayers)
        {
            TileBase tb2 = t.GetTile(new Vector3Int(node.GridPosition.x, node.GridPosition.y));
            if (tb2 != null) return true;
        }
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

}
