using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBreak : MonoBehaviour {
    private LevelManager m_LevelPieceManager;

    private void OnEnable() {
        m_LevelPieceManager = FindObjectOfType<LevelManager>();
    }


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Collided");
            m_LevelPieceManager.DestroyDoor(transform.gameObject);
        }
    }
}
