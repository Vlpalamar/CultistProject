using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveWeaponEvent  :MonoBehaviour
{
    public event Action<GiveWeaponEvent, GiveWeaponEventArgs> OnGiveWeapon;

    public void CallGiveWeaponEvent(Weapon weapon)
    {
        OnGiveWeapon?.Invoke(this, new GiveWeaponEventArgs() { Weapon = weapon }) ;
    }
}
public class GiveWeaponEventArgs : EventArgs
{
    private Weapon weapon;

    public Weapon Weapon { get => weapon; set => weapon = value; }
}
