using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IComparable<Node>
{
    private Vector2Int gridPosition;
    private int gCost = 0;//from start
    private int hCost = 0;//to finish
    private Node parentNode;


    public Vector2Int GridPosition { get => gridPosition;  set=> gridPosition= value; }

    public int HCost { get => hCost; set => hCost = value; }
    public Node ParentNode { get => parentNode; set => parentNode = value; }
    public int GCost { get => gCost; set => gCost = value; }
    public int FCost // sum
    {
        get
        {
            return GCost + HCost;
        }
    }

    

    public Node(Vector2Int gridPosition)
    {
        this.gridPosition = gridPosition;
        parentNode = null;
    }
     
 
  
    public int CompareTo(Node nodeToCompare)
    {
        int compare = FCost.CompareTo(nodeToCompare.FCost);
        if (compare==0)
        {
            compare = HCost.CompareTo(nodeToCompare.HCost);
        }

        return compare;
        
    }

     
}
