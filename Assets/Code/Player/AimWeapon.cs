using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AimWeaponEvent))]
[DisallowMultipleComponent]
public class AimWeapon : MonoBehaviour
{
    #region Aim circle Details
    [Header("Aim circle Details")]
    [Space(10)]
    #endregion
    [SerializeField] private GameObject _aimCircle;
    [SerializeField] private float _radius = 2;

   
  

    [SerializeField] private Transform weaponCursorPointTransorm;
    private AimWeaponEvent aimWeaponEvent;

    

    private void Awake()
    {
        
        aimWeaponEvent = GetComponent<AimWeaponEvent>();
        weaponCursorPointTransorm = GameObject.FindWithTag("Cursor").transform;
    }

    private void OnEnable()
    {
       
        aimWeaponEvent.OnWeaponAim += AimWeaponEvent_OnWeaponAim;
        aimWeaponEvent.OnWeaponAim += AimCircle_OnWeaponAim;
    }



    private void OnDisable()
    {
        aimWeaponEvent.OnWeaponAim -= AimWeaponEvent_OnWeaponAim;
        aimWeaponEvent.OnWeaponAim -= AimCircle_OnWeaponAim;
    }

    private void AimWeaponEvent_OnWeaponAim(AimWeaponEvent aimWeaponEvent, AimWeaponEventArgs aimWeaponEventArgs)
    {
        Aim(aimWeaponEventArgs.AimDirection, aimWeaponEventArgs.AimAngle);
    }

    private void Aim(AimDirection aimDirection, float aimAngle)
    {
        weaponCursorPointTransorm.eulerAngles = new Vector3(0f, 0f, aimAngle);
        //смена анимации на нужное направление


    }


    private void AimCircle_OnWeaponAim(AimWeaponEvent aimWeaponEvent, AimWeaponEventArgs aimWeaponEventArgs)
    {
        MoveCirle(aimWeaponEventArgs.AimAngle);

       
    }

    private void MoveCirle(float aimAngle)
    {
        print(aimAngle);

        float angl = aimAngle* Mathf.Deg2Rad;
       
        float cirlePositionX;
        float cirlePositionY;

        _aimCircle.transform.localPosition = Vector2.zero;

        cirlePositionX = _aimCircle.transform.localPosition.x + Mathf.Cos(angl) * _radius;
        cirlePositionY = _aimCircle.transform.localPosition.y + Mathf.Sin(angl) * _radius;
        _aimCircle.transform.localPosition = new Vector2(cirlePositionX, cirlePositionY);
        
    }
}
