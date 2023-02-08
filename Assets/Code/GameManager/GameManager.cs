using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] LocationBuilder locationBuilder;
    private LocationSO currentLocation;
    private Player player;
    private PlayerDetailsSO playerDetails;
    private GameState gameState;

    public GameState GameState { get => gameState; }
    public LocationSO CurrentLocation { get => currentLocation;  }

    protected override void Awake()
    {
        base.Awake();
        playerDetails = HelperUtilities.GameResources.currentPlayerDetails;
        InstantiatePlayer();


    }
    private void Start()
    {
        gameState = GameState.gameStarted;
    }

    internal Player GetPlayer()
    {
        return player;
    }

    private void Update()
    {
        HandleGameStates(gameState);
    }

    private void HandleGameStates(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.gameStarted:
                StartTheGame();

                break;
            case GameState.playingTheGame:
                
                break;
        }

    }

    private void StartTheGame()
    {
        currentLocation = locationBuilder.GenerateLocation();
        //инициализировать хаб 
        player.transform.position = CurrentLocation.spawnPoint;


        gameState = GameState.playingTheGame;
    }

    private void InstantiatePlayer()
    {
        
        GameObject playerGameObject = Instantiate(playerDetails.PlayerPrefab);
        print(playerGameObject);
        player = playerGameObject.GetComponent<Player>();
        player.Initialize(playerDetails);
    }
}
