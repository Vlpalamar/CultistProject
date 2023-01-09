using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(ItemHealth))]


[DisallowMultipleComponent]
public class Destoyable : MonoBehaviour
{
    ItemHealth health;

    private void Awake()
    {
        health = GetComponent<ItemHealth>();
    }

}
