using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "HealthDetails_", menuName = "Scriptable Objects/Health")]
public class HealthDetailsSO : ScriptableObject
{
    [SerializeField] private int startingHeath;
    [SerializeField] private int maximumHealth;

    public int StartingHeath { get => startingHeath; set => startingHeath = value; }
    public int MaximumHealth { get => maximumHealth; set => maximumHealth = value; }
}
