using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[DisallowMultipleComponent]
public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform weaponShootPosition; 
    private Player player;
   

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        MovementInput();

        AimInput();

    }

    private void AimInput()
    {
        
        float playerAngleDegrees;
        AimDirection playerAimDirection;

        AimInput( out playerAngleDegrees, out playerAimDirection);
    }

    private void AimInput( out float playerAngleDegrees, out AimDirection playerAimDirection)
    {
        Vector3 mouseWorldPosition = AimHelperUtilities.GetMouseWorldPosition();
        Vector3 playerDirection = (mouseWorldPosition - transform.position);
        playerAngleDegrees = AimHelperUtilities.GetAngleFromVector(playerDirection);
        playerAimDirection = AimHelperUtilities.GetAimDirection(playerAngleDegrees);

        player.AimWeaponEvent.CallAimWeaponEvent(playerAngleDegrees, playerAimDirection);
    }

    private void MovementInput()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(horizontalMovement, verticalMovement);

        if (horizontalMovement != 0f && verticalMovement != 0f)
            direction *= 0.7f;
        

        if(direction !=Vector2.zero)
        {
            player.MovementByVelocityEvent.CallMovementByVelocityEvent(direction, moveSpeed);
        }
        else
        {
            //idle
            player.MovementByVelocityEvent.CallMovementByVelocityEvent(direction, 0);
        }


    }
}
