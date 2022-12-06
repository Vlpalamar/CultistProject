using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[DisallowMultipleComponent]
public class PlayerControll : MonoBehaviour
{

    private Player player;
    private void Start()
    {
        player = GetComponent<Player>();
    }
    private void Update()
    {
        //мув инпут
        WeaponInput();

        //куллдаун на ролл
    
    }

    private void WeaponInput()
    {
        //если перекат - ретурн

        ActiveWeapon();


    }


    /// <summary>
    /// активировать оружие, основное свойство
    /// если огнестрел - стрелять
    /// если ближнее - бить 
    /// если магие- магичить
    /// </summary>
    public void ActiveWeapon()
    {
        if (Input.GetMouseButton(0))
        {
            print("1");
            //активировать оружие
        }
    }
}
