using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour {
    
    private const float m_PlayerSpawnTileDistance = 100f;           // Distance The player has to be to spawn new objects.

    public Transform m_TileParent;                                // parent of the tiles spawned.
    public Transform m_LevelStart;                                // Starting Tiles End Position.
    public List<Transform> m_TileList;                           // List of Tiles.
    private Vector3 m_LastTileEnd;                            // End Tiles of PreFabs.
    
    
    public GameObject m_Player;                                 // "Player"
    
    private void Awake() {
        m_LastTileEnd = m_LevelStart.position;          // Sets The last tiles position to the first End tile position.
    }

    private void Start() {
    }

    private void Update() {
        
        if (Vector3.Distance(m_Player.transform.position, m_LastTileEnd) < m_PlayerSpawnTileDistance) { // If Player get close to the spawn Tile distance.
            SpawnTile();
            // Removes first child if player if 100 unity distance from them.
            Transform[] removeTile = m_TileParent.GetComponentsInChildren<Transform>();
            GameObject removeTileFirst = removeTile[1].gameObject;
            if (Vector3.Distance(m_Player.transform.position, removeTileFirst.transform.position) > m_PlayerSpawnTileDistance ) {
                Destroy(removeTileFirst);
            }
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
        tilesTransform.SetParent(m_TileParent);                                                // sets as parent so we can remove it later on.
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
