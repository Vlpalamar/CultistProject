
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected WeaponDetailsSO weaponDetails;
     

    public virtual void Use()
    {

    }
}
