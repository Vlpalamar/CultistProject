using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementByVelocityEvent : MonoBehaviour
{
    public event Action<MovementByVelocityEvent, MovementByVelocityArgs> OnMovementByVelocity;
    public void CallMovementByVelocityEvent(Vector2 moveDirection, float moveSpeed)
    {
        OnMovementByVelocity?.Invoke(this, new MovementByVelocityArgs() { MoveDirection = moveDirection, MoveSpeed = moveSpeed });
    }
}

public class MovementByVelocityArgs: EventArgs
{
    private Vector2 moveDirection;
    private float moveSpeed;

    public Vector2 MoveDirection { get => moveDirection; set => moveDirection = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
}