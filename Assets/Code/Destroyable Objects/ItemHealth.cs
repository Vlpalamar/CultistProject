using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealth : Health
{
    Destoyable item;
    private void Start()
    {
        item = GetComponent<Destoyable>();
    }
    protected override void Die()
    {
        //анимация разрушения
        //удаление коллайдера(возможно)
        //удаление риджетбади(возможно)
    }

}
