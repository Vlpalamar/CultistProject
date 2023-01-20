using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementDetails_", menuName = "Scriptable Objects/MovementDetails")]
public class MovementDetailsSO : ScriptableObject
{

    [SerializeField]private float moveSpeed;


    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
}
