using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour {
    
    private const float m_PlayerSpawnTileDistance = 200f;           // Distance The player has to be to spawn new objects.
    private const float m_PlayerRemovalDistance = 100f;

    public Transform m_LevelStart;                                // Starting Tiles End Position.
    public List<Transform> m_TileList;                           // List of Tiles.
    private Vector3 m_LastTileEnd;                            // End Tiles of PreFabs.
    

    public GameObject m_powerUp;
    public GameObject m_Player;                                 // "Player"
    
    private void Awake() {
        m_LastTileEnd = m_LevelStart.position;          // Sets The last tiles position to the first End tile position.
    }

    private void Start() {
    }

    private void Update() {
        
        if (Vector3.Distance(m_Player.transform.position, m_LastTileEnd) < m_PlayerSpawnTileDistance) { // If Player get close to the spawn Tile distance.
            SpawnTile();
            //m_RemoveTile = GameObject.FindWithTag("Tile").GetComponent<RemoveTile>();
        }
        else if (Vector3.Distance(m_Player.transform.position, m_LastTileEnd) > m_PlayerRemovalDistance) {
            //StartCoroutine(m_RemoveTile.DestroyTile());
        }
    }


    /// <summary>
    /// Picks a Random Tile from Tile List, Then.
    /// Spawns a Random Tile at Last Position(Object Named "EndPosition") + Random Tile first object z extents.
    /// </summary>
    private void SpawnTile() {
        Transform randomTile = m_TileList[Random.Range(0, m_TileList.Count)];                                            // Picks a Random Transform from RandomTile<List>
        Vector3 tileExtents = randomTile.GetComponentInChildren<MeshRenderer>().bounds.extents;
        m_LastTileEnd.z += tileExtents.z;
        m_LastTileEnd.y = 0f;
        Transform lastTilePos = SpawnTile(randomTile, m_LastTileEnd);                                                 // Sets SpawnTile Parameters.
        m_LastTileEnd = lastTilePos.Find("EndPosition").position ;                                                   // Finds a new lastPosition.
    }

    /// <summary>
    /// Instantiates with set Parameters when used.
    /// </summary>
    /// <param name="tileEnd"></param>
    /// <param name="spawnPos"></param>
    /// <returns>A instantiate(tileEnd, spawnPos)</returns>
    private Transform SpawnTile(Transform tileEnd, Vector3 spawnPos) {
        Transform tilesTransform = Instantiate(tileEnd, spawnPos, Quaternion.identity);         // Instantiates tiles.
        return tilesTransform;
    }

    /// <summary>
    /// Removes "Door" So player doesnt collider.
    /// </summary>
    /// <param name="Door"></param>
    public void DestroyDoor(GameObject Door) {
        Destroy(Door);
    }

}
