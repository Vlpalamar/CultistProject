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
        //��� �����
        WeaponInput();

        //�������� �� ����
    
    }

    private void WeaponInput()
    {
        //���� ������� - ������

        ActiveWeapon();


    }


    /// <summary>
    /// ������������ ������, �������� ��������
    /// ���� ��������� - ��������
    /// ���� ������� - ���� 
    /// ���� �����- ��������
    /// </summary>
    public void ActiveWeapon()
    {
        if (Input.GetMouseButton(0))
        {
            print("1");
            //������������ ������
        }
    }
}
