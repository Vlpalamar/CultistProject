using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AimWeaponEvent))]
[DisallowMultipleComponent]
public class AimWeapon : MonoBehaviour
{
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
    }

    private void OnDisable()
    {
        aimWeaponEvent.OnWeaponAim -= AimWeaponEvent_OnWeaponAim;
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

   

}
