using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour {
    private bool m_LoadComponents;
    
    // Cameras.

    public Camera m_MainCamera;


    // Text.
    public TextMeshProUGUI m_ScoreText;
    
    
    
    
    // GameObjects.
    public GameObject m_Player;         // Player Gameobject

    
    // Other Managers.
    public GameObject m_LevelManager;
    
    /// <summary>
    /// States of the game.
    /// </summary>
    public enum GameState {
        Start,
        Playing,
        GameOver
    };
    private GameState m_GameState;
    public GameState State { get { return m_GameState; } }          // Displays State On Unity.


    private void Awake() {
        m_Player.SetActive(false);
        m_ScoreText.gameObject.SetActive(false);
        m_GameState = GameState.Start;


    }

    private void Update() {
        switch (m_GameState) {
            case GameState.Start:
                m_LevelManager.SetActive(true);
                    m_GameState = GameState.Playing;
                break;
            case GameState.Playing:
                m_ScoreText.gameObject.SetActive(true);
                m_Player.SetActive(true);
                if (PlayerDead()) {
                m_GameState = GameState.GameOver;
            }
                if (Input.GetKeyDown(KeyCode.R)) {
                    Scene scene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(scene.name);
                }

                break;
            case GameState.GameOver:
                
                
                if (Input.GetKeyDown(KeyCode.R)) {
                    Scene scene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(scene.name);
                }
                break;
        }
        if (Input.GetKeyUp(KeyCode.Escape)) {
            Application.Quit();
        }

    }
    /// <summary>
    /// Checks if Players dead.
    /// </summary>
    /// <returns></returns>
    private bool PlayerDead() {
        if (m_Player.transform.position.y <= -8f) {
            m_Player.SetActive(false);
            Camera cam = m_Player.GetComponentInChildren<Camera>();
            cam.transform.parent = null;
            return true;
        }
        return false;
    }

}

