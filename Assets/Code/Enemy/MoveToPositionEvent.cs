using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPositionEvent : MonoBehaviour
{
    public event Action<MoveToPositionEvent, MoveToPositionArgs> OnMoveToPosition;

    public void CallOnMoveToPositionEvent(Vector3 startPosition, Vector3 endPosition, Vector2 movDirection, float moveSpeed)
    {
        OnMoveToPosition?.Invoke(this, new MoveToPositionArgs { currentPosition = startPosition, endPosition = endPosition,  moveDirection = movDirection, moveSpeed = moveSpeed });
    }
}

public class MoveToPositionArgs : EventArgs
{
    public Vector3 currentPosition;
    public Vector3 endPosition;
    public Vector2 moveDirection;
    public float moveSpeed;
}