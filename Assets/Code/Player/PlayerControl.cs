using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[DisallowMultipleComponent]
public class PlayerControl : MonoBehaviour
{
    
    private Coroutine _playerRollCoroutine;
    private WaitForFixedUpdate _waitForFixedUpdate;
    public AimDirection _aimDirection;
    

    

    private Player _player;


    private void Start()
    {
        _waitForFixedUpdate = new WaitForFixedUpdate();
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
        if (_player.Weapon.Weapon == null) return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
            _player.AnimatePlayer.SetAttackAnimation(_aimDirection, _player.AnimatePlayer.AnimationAttack, false, 1f);

    }

    private void AimInput()
    {
        
        float playerAngleDegrees;
        

        AimInput( out playerAngleDegrees, out _aimDirection);

    }

    private void AimInput( out float playerAngleDegrees, out AimDirection playerAimDirection)
    {
        Vector3 mouseWorldPosition = HelperUtilities.GetMouseWorldPosition();
        Vector3 playerDirection = (mouseWorldPosition - transform.position);
        playerAngleDegrees = HelperUtilities.GetAngleFromVector(playerDirection);
        playerAimDirection = HelperUtilities.GetAimDirection(playerAngleDegrees);

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
                
                _player.MovementByVelocityEvent.CallMovementByVelocityEvent(direction, _player.MovementDetails.MoveSpeed);
            }
            else
            {
                if (_player.Roll.IsReady && _player.Roll.RollsRemaining>0)
                {
                    if (!_player.PlayerStamina.TryToUse(_player.Roll.StaminaCost)) return;
                  

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
            _player.IdleEvent.CallIdleEvent();
           // _player.MovementByVelocityEvent.CallMovementByVelocityEvent(direction, 0);
        }


    }

    private void Roll(Vector3 direction)
    {
        _playerRollCoroutine = StartCoroutine(PlayerRollRoutine(direction));
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
            
            yield return _waitForFixedUpdate;
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


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.GetComponent<Pickable>())
            collision.collider.GetComponent<Pickable>().PickUp();
        
    }
}
