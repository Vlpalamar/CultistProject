using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LocationBuilder LocationBuilder;
    private void Start()
    {
        LocationBuilder.GenerateLocation();
    }

}
