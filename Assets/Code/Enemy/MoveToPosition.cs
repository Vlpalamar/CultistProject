using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveToPositionEvent))]
[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
public class MoveToPosition : MonoBehaviour
{
    private MoveToPositionEvent moveToPositionEvent;
    private Rigidbody2D rigidbody;



    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        moveToPositionEvent = GetComponent<MoveToPositionEvent>();
      
    }


    


    private void OnEnable()
    {
        moveToPositionEvent.OnMoveToPosition += MoveToPositionEvent_OnMoveToPosition;
    }

    private void OnDisable()
    {
        moveToPositionEvent.OnMoveToPosition += MoveToPositionEvent_OnMoveToPosition;
    }

    private void MoveToPositionEvent_OnMoveToPosition(MoveToPositionEvent rollEvent, MoveToPositionArgs rollArgs)
    {
        MoveToPoint(rollArgs.endPosition, rollArgs.currentPosition, rollArgs.moveSpeed);
    }

    private void MoveToPoint(Vector3 endPosition, Vector3 currentPosition, float moveSpeed)
    {
        Vector2 unitVector = Vector3.Normalize(endPosition - currentPosition);

        rigidbody.MovePosition(rigidbody.position + (unitVector * moveSpeed * Time.deltaTime));

    }
}
