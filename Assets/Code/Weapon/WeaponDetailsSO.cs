using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon_", menuName = "Scriptable Objects/Weapon")]
public class WeaponDetailsSO : ScriptableObject
{
    [SerializeField] private string weaponName;
    [SerializeField] private float range;
    [SerializeField] private float delay ;
    [SerializeField] private float damage;
    [SerializeField] private float preCharge = 0;
    [SerializeField] private Sprite icon;

    public string WeaponName { get => weaponName;  }
    public float Range { get => range;  }
    public float Damage { get => damage; }
    public float PreCharge { get => preCharge; }
    public Sprite Icon { get => icon; set => icon = value; }
    public float Delay { get => delay; set => delay = value; }
}
