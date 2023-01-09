using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon_", menuName = "Scriptable Objects/Weapon")]
public class WeaponDetailsSO : ScriptableObject
{
    [SerializeField] private string weaponName;
    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private float preCharge = 0;

    public string WeaponName { get => weaponName;  }
    public float Range { get => range;  }
    public float Damage { get => damage; }
    public float PreCharge { get => preCharge; }

}
