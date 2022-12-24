using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class AStar  :MonoBehaviour
{

    List<Node> _openList = new List<Node>();
    Node _activeNode;
    bool _isOver = false;

    Queue<Vector2Int> readyQueue;
    public Queue<Vector2Int> ReadyQueue { get => readyQueue;  }

    public AStar()
    {
        readyQueue = new Queue<Vector2Int>();
    }

    public Queue<Vector2Int> BuidlPath(Vector2Int startGridPosition, Vector2Int endGridPosition)
    {
        //поставить точку
        _activeNode = new Node(startGridPosition);

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
            readyQueue.Enqueue(_activeNode.GridPosition);


        } while (!_isOver );

        Debug.Log("Done");
        return readyQueue;
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


                _openList.Add(node);

            }
        }
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
