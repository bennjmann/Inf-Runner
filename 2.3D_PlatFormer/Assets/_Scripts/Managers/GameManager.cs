using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private bool m_LoadComponents;
    
    // Cameras.

    public Camera m_MainCamera;


    // Text.
    public Canvas m_DeathScreen;
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
        m_ScoreText.gameObject.SetActive(true);
        m_GameState = GameState.Start;


    }

    private void Update() {
        switch (m_GameState) {
            case GameState.Start:
                m_LevelManager.SetActive(true);
                    m_GameState = GameState.Playing;
                break;
            case GameState.Playing:
                m_Player.SetActive(true);
                if (m_Player.GetComponent<PlayerController>().m_IsDead == true) {
                    PlayerDead();
                     m_GameState = GameState.GameOver;
            }
                if (Input.GetKeyDown(KeyCode.R)) {
                    Scene scene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(scene.name);
                }

                break;
            case GameState.GameOver:
                m_ScoreText.gameObject.SetActive(true);


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
    public void PlayerDead() {
        m_Player.SetActive(false);
        Camera cam = m_Player.GetComponentInChildren<Camera>();
        cam.transform.parent = null;
        cam.gameObject.SetActive(true);
        m_DeathScreen.gameObject.SetActive(true);
    }

    
    /// <summary>
    /// Calls Load scene MainMenu.
    /// </summary>
    public void MainMenu() { SceneManager.LoadScene("MainMenu"); }
    /// <summary>
    /// Loads "GamePlay" Scene.
    /// </summary>
    void Restart() { SceneManager.LoadScene("GamePlay");}
}


