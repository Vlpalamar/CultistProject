using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Arthefact : MonoBehaviour
{
    [SerializeField] Sprite icon;

    [SerializeField] protected bool isAlways;
    protected Player _player;
    

    public bool IsAlways { get => isAlways; set => isAlways = value; }
    public Sprite Icon { get => icon;  }

    public abstract void TakeTheArthefact();

    public abstract void DropTheArthefact();

    public abstract void UseAlways();
}
