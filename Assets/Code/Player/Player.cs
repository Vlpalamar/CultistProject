using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(MovementByVelocity))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(AimWeapon))]
[RequireComponent(typeof(AimWeaponEvent))]

[RequireComponent(typeof(PlayerControl))]
public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidbody;
    private Health health;
    private Animator animator;
    private PlayerDetailsSO playerDetails;
    private MovementByVelocityEvent movementByVelocityEvent;
    private AimWeaponEvent aimWeaponEvent;
    private PlayerControl playerControl;
    public MovementByVelocityEvent MovementByVelocityEvent { get => movementByVelocityEvent;}
    public AimWeaponEvent AimWeaponEvent { get => aimWeaponEvent; }

    //private PlayerControll playerControll;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        playerControl= GetComponent<PlayerControl>();
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        aimWeaponEvent = GetComponent<AimWeaponEvent>();

    }
    

    public void Initialize(PlayerDetailsSO playerDetails)
    {
        this.playerDetails = playerDetails;
       
        SetPlayerHealth();
    }

    private void SetPlayerHealth()
    {

        //print(health);
        health.SetStartingHealth(playerDetails.PlayerStartHealth);
    }
}
