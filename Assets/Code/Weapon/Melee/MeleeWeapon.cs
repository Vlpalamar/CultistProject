using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeWeapon : Weapon
{
    protected MeleeWeapon(WeaponDetailsSO weaponDetailsSO) : base(weaponDetailsSO)
    {
    }
}
