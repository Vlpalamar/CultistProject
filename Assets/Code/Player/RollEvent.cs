using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class RollEvent : MonoBehaviour
{

    public event Action<RollEvent, RollArgs> OnRoll;

    public void CallOnRollEvent(Vector3 startPosition, Vector3 endPosition, Vector2 movDirection, float rollSpeed, bool isRolling)
    {
        OnRoll?.Invoke(this, new RollArgs {currentPosition=startPosition, endPosition= endPosition, isRolling= isRolling, moveDirection = movDirection, rollSpeed= rollSpeed});
    }
}

public class RollArgs : EventArgs
{
    public Vector3 currentPosition;
    public Vector3 endPosition;
    public Vector2 moveDirection;
    public float rollSpeed;
    public bool isRolling;


}

