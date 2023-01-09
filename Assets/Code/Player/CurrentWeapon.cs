using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentWeapon : MonoBehaviour
{
    Weapon weapon;

    public Weapon Weapon { get => weapon; set => weapon = value; }

    private void Start()
    {
        
    }

    public void Use()
    {
        Weapon.Use();
    }

    public void ChangeWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;
    }
}
