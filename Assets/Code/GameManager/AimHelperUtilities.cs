using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class AimHelperUtilities : MonoBehaviour
{
    private static Camera mainCamera;
    public static Vector3 GetMouseWorldPosition()
    {
        if (mainCamera == null) mainCamera = Camera.main;

        Vector3 mouseScreenPosition = Input.mousePosition;

        mouseScreenPosition.x = Mathf.Clamp(mouseScreenPosition.x, 0f, Screen.width);
        mouseScreenPosition.y = Mathf.Clamp(mouseScreenPosition.y, 0f, Screen.height);

        
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        worldPosition.z = 0;
        return worldPosition;
    }

    private void Awake()
    {
        
    }
    public static float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);
        float degrees = radians * Mathf.Rad2Deg;

        return degrees;
    }

    public static AimDirection GetAimDirection(float playerAngleDegrees)
    {
        if (playerAngleDegrees>=22 && playerAngleDegrees<= 67 )
        {
            return AimDirection.topRight;
        }
        if (playerAngleDegrees >= 67 && playerAngleDegrees <= 112)
        {
            return AimDirection.top;
        }
        if (playerAngleDegrees >= 112 && playerAngleDegrees <= 158)
        {
            return AimDirection.topLeft;
        }
        if (playerAngleDegrees >= -23 && playerAngleDegrees <= 22)
        {
            return AimDirection.right;
        }
        if (playerAngleDegrees >= -68 && playerAngleDegrees <= -23)
        {
            return AimDirection.downRight;
        }
        if (playerAngleDegrees >= -113 && playerAngleDegrees <= -68)
        {
            return AimDirection.down;

        }
        if (playerAngleDegrees >= -158 && playerAngleDegrees <= -113)
        {
            return AimDirection.downLeft;
        }
        else
        {
            return AimDirection.left;
        }

    }
}
