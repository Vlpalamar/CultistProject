using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

[DisallowMultipleComponent]
public abstract class Pickable : MonoBehaviour
{
    [SerializeField]protected string name;
    [SerializeField] protected GameObject prefab;

    protected Player _player;

    public string Name { get => name;  }
    public GameObject Prefab { get => prefab;  }

    public virtual void Start()
    {
        _player = GameManager.Instance.GetPlayer();
    }
    abstract public void PickUp();
}
