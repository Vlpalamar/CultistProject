using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(GiveWeaponEvent))]
[DisallowMultipleComponent]
public class GiveWeapon : MonoBehaviour
{

    private GiveWeaponEvent giveWeaponEvent;

    private void Awake()
    {
        giveWeaponEvent = GetComponent<GiveWeaponEvent>();
    }

    private void OnEnable()
    {
        giveWeaponEvent.OnGiveWeapon += GiveWeaponEvent_OnGiveWeapon;

    }

   

    private void OnDisable()
    {
        giveWeaponEvent.OnGiveWeapon -= GiveWeaponEvent_OnGiveWeapon;
    }

    private void GiveWeaponEvent_OnGiveWeapon(GiveWeaponEvent giveWeapon, GiveWeaponEventArgs args)
    {
        PassTheWeapon(args.Weapon);
      
    }

    private void PassTheWeapon(Weapon weapon)
    {
        GameManager.Instance.GetPlayer().Weapon.ChangeWeapon(weapon);
        print("!");
    }
}
