using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MagicWeapon : Weapon
{
    [SerializeField]
    protected int manaCost;

    protected MagicWeapon(WeaponDetailsSO weaponDetailsSO) : base(weaponDetailsSO)
    {
    }
}
