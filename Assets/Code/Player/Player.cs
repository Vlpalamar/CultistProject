using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(MovementByVelocity))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(AimWeapon))]
[RequireComponent(typeof(AimWeaponEvent))]
[RequireComponent(typeof(CurrentWeapon))]
[RequireComponent(typeof(Roll))]
[RequireComponent(typeof(RollEvent))]

[RequireComponent(typeof(PlayerControl))]
public class Player : MonoBehaviour
{
    [SerializeField]private MovementDetailsSO movementDetails;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidbody;
    private PlayerHealth health;
    private Animator animator;
    private PlayerDetailsSO playerDetails;
    private MovementByVelocityEvent movementByVelocityEvent;
    private AimWeaponEvent aimWeaponEvent;
    private PlayerControl playerControl;
    private CurrentWeapon weapon;
    private Roll roll;
    private RollEvent rollEvent;


    public MovementByVelocityEvent MovementByVelocityEvent { get => movementByVelocityEvent;}
    public AimWeaponEvent AimWeaponEvent { get => aimWeaponEvent; }
    public CurrentWeapon Weapon { get => weapon; set => weapon = value; }
    public Roll Roll { get => roll; }
    public RollEvent RollEvent { get => rollEvent;  }
    public Rigidbody2D Rigidbody { get => rigidbody; set => rigidbody = value; }
    public MovementDetailsSO MovementDetails { get => movementDetails;}

    //private PlayerControll playerControll;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<PlayerHealth>();
        playerControl= GetComponent<PlayerControl>();
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        aimWeaponEvent = GetComponent<AimWeaponEvent>();
        weapon = GetComponent<CurrentWeapon>();
        roll = GetComponent <Roll>();
        rollEvent = GetComponent<RollEvent>();

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
