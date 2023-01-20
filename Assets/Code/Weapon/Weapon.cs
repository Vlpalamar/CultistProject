
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected WeaponDetailsSO weaponDetails;

    private bool isReady = false;
    private float currentTimeTillAttack=0;


    public WeaponDetailsSO WeaponDetails { get => weaponDetails; set => weaponDetails = value; }
    public bool IsReady { get => isReady; set => isReady = value; }
    public float CurrentTimeTillAttack { get => currentTimeTillAttack; set => currentTimeTillAttack = value; }

    protected  Weapon(WeaponDetailsSO weaponDetailsSO)
    {
        weaponDetails = weaponDetailsSO;
    }

    public virtual void Use()
    {

    }
    public virtual void Use(AimDirection aimDirection)
    {

    }
}
