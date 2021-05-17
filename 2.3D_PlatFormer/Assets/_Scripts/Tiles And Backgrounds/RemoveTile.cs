using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveTile : MonoBehaviour {
    private LevelManager m_LevelManager;


    private void Awake() {
        m_LevelManager = FindObjectOfType<LevelManager>();
    }
    
    public IEnumerator DestroyTile() {
        Debug.Log("Tile Removing");
        yield return new WaitForSeconds(10);
        m_LevelManager.TileDestroy(gameObject);
    }

}
