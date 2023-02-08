using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class HelperUtilities : MonoBehaviour
{
    private static Camera mainCamera;

    private static GameResources gameResources;

    public static GameResources GameResources
    {
        get 
        {
            if (gameResources==null)
            {
                gameResources = Resources.Load<GameResources>("GameResources_");
                return gameResources;
            }
            else
            {
                return gameResources;
            }
        }  
    }

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

   public static void DrowRectangle(Vector2 top_right_corner, Vector2 bottom_left_corner )
    {
        Vector2 center_offset = (top_right_corner + bottom_left_corner) * 0.5f;
        Vector2 displacement_vector = top_right_corner - bottom_left_corner;
        float x_projection = Vector2.Dot(displacement_vector, Vector2.right);
        float y_projection = Vector2.Dot(displacement_vector, Vector2.up);

        Vector2 top_left_corner = new Vector2(-x_projection * 0.5f, y_projection * 0.5f)+ center_offset;
        Vector2 bottom_right_corner = new Vector2(x_projection * 0.5f, -y_projection * 0.5f)+ center_offset;

        Gizmos.DrawLine(top_right_corner, top_left_corner);
        Gizmos.DrawLine(top_left_corner, bottom_left_corner);
        Gizmos.DrawLine(bottom_left_corner, bottom_right_corner);
        Gizmos.DrawLine(bottom_right_corner, top_right_corner);

    }
    
    public static float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);
        float degrees = radians * Mathf.Rad2Deg;

        return degrees;
    }

    public static float LinearToDecibels(int liner)
    {
        float linerScaleRange = 20f;

        return Mathf.Log10((float)liner / linerScaleRange) * 20f;
    }

    public static AimDirection GetAimDirection(float playerAngleDegrees)
    {
        //8сторон

        //    if (playerAngleDegrees>=22 && playerAngleDegrees<= 67 )
        //    {
        //        return AimDirection.topRight;
        //    }
        //    if (playerAngleDegrees >= 67 && playerAngleDegrees <= 112)
        //    {
        //        return AimDirection.top;
        //    }
        //    if (playerAngleDegrees >= 112 && playerAngleDegrees <= 158)
        //    {
        //        return AimDirection.topLeft;
        //    }
        //    if (playerAngleDegrees >= -23 && playerAngleDegrees <= 22)
        //    {
        //        return AimDirection.right;
        //    }
        //    if (playerAngleDegrees >= -68 && playerAngleDegrees <= -23)
        //    {
        //        return AimDirection.downRight;
        //    }
        //    if (playerAngleDegrees >= -113 && playerAngleDegrees <= -68)
        //    {
        //        return AimDirection.down;

        //    }
        //    if (playerAngleDegrees >= -158 && playerAngleDegrees <= -113)
        //    {
        //        return AimDirection.downLeft;
        //    }
        //    else
        //    {
        //        return AimDirection.left;
        //    }

        //4 стороны 
        if (playerAngleDegrees >= 45 && playerAngleDegrees <= 135)
        {
            return AimDirection.top;
        }
        if (playerAngleDegrees >= -45 && playerAngleDegrees <= 45)
        {
              return AimDirection.right;
        }
        if (playerAngleDegrees >= -135 && playerAngleDegrees <= -45)
        {
            return AimDirection.down ;
        }
        else 
        {
            return AimDirection.left;
        }

    }
}
