using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[DisallowMultipleComponent]
public class InstantiatedEvent : MonoBehaviour
{
    #region TilemapsTags Consts
    private const string GROUNDTILEMAPTAG = "GroundTileMap";
    private const string DECORATION1TILEMAPTAG = "Decoration1TileMap";
    private const string DECORATION2TILEMAPTAG = "Decoration2TileMap";
    private const string FRONTTILEMAPTAG = "FrontTileMap";
    private const string COLLISIONTILEMAPTAG = "ColisionTileMap";
    private const string MINIMAPTILEMAPTAG = "MinimapTileMap";
    private const string AIMOVEMENT = "AIMovement"; 


    #endregion

     [HideInInspector] public Grid grid;
    [HideInInspector] public Tilemap groundTileMap;
    [HideInInspector] public Tilemap decoration1TileMap;
    [HideInInspector] public Tilemap decoration2TileMap;
    [HideInInspector] public Tilemap frontTileMap;
    [HideInInspector] public Tilemap collisionTileMap;
    [HideInInspector] public Tilemap minimapTileMap;
    [HideInInspector] public Tilemap aiMovement;
    [HideInInspector] public Bounds bounds;
    public Transform gameObjectLocationTransform;
    private BoxCollider2D boxCollider2D;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        bounds = boxCollider2D.bounds;
    }



   public void Initialise(GameObject roomGameObject)
   {
        PopulateTilemapVariables(roomGameObject);

        DisableCollisionTilemapRenderer();

         
   }

   

    private void PopulateTilemapVariables(GameObject roomGameObject)
    {
        grid = roomGameObject.GetComponentInChildren<Grid>();

        Tilemap[] tilemaps = roomGameObject.GetComponentsInChildren<Tilemap>();
        

        foreach (Tilemap tilemap in tilemaps)
        {
            if (tilemap.gameObject.CompareTag(GROUNDTILEMAPTAG))
            {
                groundTileMap = tilemap;
            }
            if (tilemap.gameObject.CompareTag(DECORATION1TILEMAPTAG))
            {
                decoration1TileMap = tilemap;
            }
            if (tilemap.gameObject.CompareTag(DECORATION2TILEMAPTAG))
            {
                decoration2TileMap = tilemap;
            }
            if (tilemap.gameObject.CompareTag(FRONTTILEMAPTAG))
            {
                frontTileMap = tilemap;
            }
            if (tilemap.gameObject.CompareTag(COLLISIONTILEMAPTAG))
            {
                collisionTileMap = tilemap;
            }
            if (tilemap.gameObject.CompareTag(MINIMAPTILEMAPTAG))
            {
                minimapTileMap = tilemap;
            }
            if (tilemap.gameObject.CompareTag(AIMOVEMENT))
            {
                aiMovement = tilemap;
            }


        }
    }

    public void DisableCollisionTilemapRenderer()
    {
        aiMovement.gameObject.GetComponent<TilemapCollider2D>().enabled = false;
    }

}
