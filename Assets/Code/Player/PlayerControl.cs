using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[DisallowMultipleComponent]
public class PlayerControl : MonoBehaviour
{
    #region Roll Details
    [Space(10)]
    [Header("Move Details")]
    #endregion
    [SerializeField] private float moveSpeed;
    // [SerializeField] private Transform weaponShootPosition; 

   


    private Coroutine playerRollCoroutine;
    private WaitForFixedUpdate waitForFixedUpdate;
    

    private Player _player;


    private void Start()
    {
        waitForFixedUpdate = new WaitForFixedUpdate();
    }

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (_player.Roll.IsRolling) return;
        MovementInput();

        AimInput();

        WeaponUse();

       
    }

    private void WeaponUse()
    {
        if (Input.GetKey(KeyCode.Mouse0))
            _player.Weapon.Use();      
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

        _player.AimWeaponEvent.CallAimWeaponEvent(playerAngleDegrees, playerAimDirection);
    }

    private void MovementInput()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");
        bool isSpaceButtonDown = Input.GetKeyDown(KeyCode.Space);

        Vector2 direction = new Vector2(horizontalMovement, verticalMovement);

        if (horizontalMovement != 0f && verticalMovement != 0f)
            direction *= 0.7f;
        

        if(direction !=Vector2.zero)
        {
            if (!isSpaceButtonDown)
            {
                _player.MovementByVelocityEvent.CallMovementByVelocityEvent(direction, moveSpeed);
            }
            else
            {
                if (_player.Roll.IsReady && _player.Roll.RollsRemaining>0)
                {
                   
                    _player.Roll.IsReady = false;
                    _player.Roll.IsIntersepted = false;
                    _player.Roll.RollsRemaining--;
                    Invoke(nameof(RollLocalCooldownReCharge), _player.Roll.LocalRollCooldown);
                    Invoke(nameof(RollGlobalCooldownReCharge), _player.Roll.GlobalRollCooldown);
                    Roll((Vector3) direction);
                }
                

            }
            
        }
        else
        {
            //idle
            _player.MovementByVelocityEvent.CallMovementByVelocityEvent(direction, 0);
        }


    }

    private void Roll(Vector3 direction)
    {
        print(_player.Roll.RollsRemaining);

        playerRollCoroutine = StartCoroutine(PlayerRollRoutine(direction));

    }

    private IEnumerator PlayerRollRoutine(Vector3 direction)
    {
        float minDistance = 0.3f;
        _player.Roll.IsRolling = true;
        Vector3 targetPosition = _player.transform.position + (Vector3)direction * _player.Roll.RollDistance;
        int stepsAmount = Mathf.FloorToInt(Vector3.Distance
            (_player.transform.position, targetPosition) / minDistance);
        int i=0;
        while (stepsAmount > i )
        {
            _player.RollEvent.CallOnRollEvent(_player.transform.position, targetPosition, direction, _player.Roll.RollSpeed, _player.Roll.IsRolling);
            
            yield return waitForFixedUpdate;
            i++;

        }
        _player.Roll.IsRolling = false;
       
    }

    private void RollLocalCooldownReCharge()
    {
        _player.Roll.RollLocalCooldownReCharge();
    }

    private void RollGlobalCooldownReCharge()
    {
        _player.Roll.RollGlobalCooldownReCharge();
    }

    
   
}
