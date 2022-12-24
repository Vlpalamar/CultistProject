using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimWeaponEvent : MonoBehaviour
{
    public event Action<AimWeaponEvent, AimWeaponEventArgs> OnWeaponAim;

    public void CallAimWeaponEvent( float aimAngle, AimDirection aimDirection)
    {
        OnWeaponAim?.Invoke(this, new AimWeaponEventArgs() { AimAngle = aimAngle, AimDirection = aimDirection,  });
    }
}

public class AimWeaponEventArgs:EventArgs
{
    private float aimAngle;
    private AimDirection aimDirection;

    public float AimAngle { get => aimAngle; set => aimAngle = value; }
    public AimDirection AimDirection { get => aimDirection; set => aimDirection = value; }
}