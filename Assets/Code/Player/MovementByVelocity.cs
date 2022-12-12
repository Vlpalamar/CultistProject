using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[DisallowMultipleComponent]
public class MovementByVelocity : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private MovementByVelocityEvent movementByVelocityEvent;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
    }

    private void OnEnable()
    {
        movementByVelocityEvent.OnMovementByVelocity += MovementByVelocityEvent_OnMovementByVelocity;
    }
    private void OnDisable()
    {
        movementByVelocityEvent.OnMovementByVelocity -= MovementByVelocityEvent_OnMovementByVelocity;
    }

    private void MovementByVelocityEvent_OnMovementByVelocity(MovementByVelocityEvent movementByVelocityEvent, MovementByVelocityArgs movementByVelocityArgs)
    {
        Move(movementByVelocityArgs.MoveDirection,movementByVelocityArgs.MoveSpeed);
    }

    private void Move(Vector2 moveDirection, float moveSpeed)
    {
        rigidbody.velocity = moveDirection * moveSpeed; 
    }
}
