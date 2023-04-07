using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(IdleEvent))]
public class Idle : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private IdleEvent idleEvent;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        idleEvent = GetComponent<IdleEvent>();
    }

    private void OnEnable()
    {
        idleEvent.OnIdle += IdleEvent_OnIdle;
    }

    private void OnDisable()
    {
        idleEvent.OnIdle -= IdleEvent_OnIdle;
    }
    private void IdleEvent_OnIdle(IdleEvent obj)
    {
        MoveRigidBody();
    }

    private void MoveRigidBody()
    {
        rigidbody.velocity = Vector2.zero;
    }
}
