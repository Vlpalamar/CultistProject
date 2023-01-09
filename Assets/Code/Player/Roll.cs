using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(RollEvent))]
[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
public class Roll : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private RollEvent rollEvent;

    #region Roll Details
    [Space(10)]
    [Header("Roll Details")]
    #endregion
    [SerializeField] private int amountOfRolls;
    [SerializeField] private float rollSpeed;
    [SerializeField] private float rollDistance;


    #region ToolTip
    [Tooltip("Time how ofen u can call Roll after each outher")]
    #endregion
    [SerializeField] private float globalRollCooldown;

    #region ToolTip
    [Tooltip("Cooldown Time of each Rool")]
    #endregion
    [SerializeField] private float localRollCooldown;

    private bool isRoll;
    private bool isIntersepted;
    private bool isReady;
    private int rollsRemaining ;


    public int AmountOfRolls { get => amountOfRolls; }
    public float RollSpeed { get => rollSpeed; }
    public float RollDistance { get => rollDistance; }
    public float GlobalRollCooldown { get => globalRollCooldown; }
    public float LocalRollCooldown { get => localRollCooldown; }
    public bool IsRolling { get => isRoll; set => isRoll = value; }
    public int RollsRemaining { get => rollsRemaining; set => rollsRemaining = value; }
    public bool IsReady { get => isReady; set => isReady = value; }
    public bool IsIntersepted { get => isIntersepted; set => isIntersepted = value; }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rollEvent = GetComponent<RollEvent>();
        rollsRemaining = amountOfRolls;
        IsReady = true;
        isIntersepted = false;
    }


    public void RollLocalCooldownReCharge()
    {
        IsReady = true;
    }
    public void RollGlobalCooldownReCharge()
    {
        rollsRemaining++;
    }


    private void OnEnable()
    {
        rollEvent.OnRoll += RollEvent_OnRoll;
    }

    private void OnDisable()
    {
        rollEvent.OnRoll += RollEvent_OnRoll;
    }

    private void RollEvent_OnRoll(RollEvent rollEvent, RollArgs rollArgs)
    {
        MoveToPoint(rollArgs.endPosition, rollArgs.currentPosition, rollArgs.rollSpeed);
    }

    private void MoveToPoint(Vector3  endPosition, Vector3 currentPosition, float rollSpeed)
    {
        Vector2 unitVector = Vector3.Normalize(endPosition - currentPosition);

        rigidbody.MovePosition(rigidbody.position + (unitVector * rollSpeed * Time.deltaTime));
        
    }
}
