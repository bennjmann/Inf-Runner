using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
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
    public enum GameState {                                        // State Gameplay is in.
        Start,
        Playing,
        GameOver
    };
    private GameState m_GameState;
    public GameState State { get { return m_GameState; } }          // Displays State On Unity.


    private void Awake() {
        m_Player.SetActive(false);                  // Disables player at the start If not gets stuck.
        m_ScoreText.gameObject.SetActive(true);    // Enables Score GUi.
        m_GameState = GameState.Start;


    }

    private void Update() {
        switch (m_GameState) {
            case GameState.Start:
                m_LevelManager.SetActive(true);                 // Turns on the levels and sets to playing.
                    m_GameState = GameState.Playing;
                break;
            case GameState.Playing:
                m_Player.SetActive(true);                                           // Sets player true.
                if (m_Player.GetComponent<PlayerController>().m_IsDead == true) {   // Check if player is dead.
                    PlayerDead();                       
                     m_GameState = GameState.GameOver;
            }
                if (Input.GetKeyDown(KeyCode.R)) {                  // Restarts the game if r is pressed
                    Scene scene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(scene.name);
                }

                break;
            case GameState.GameOver:
                m_ScoreText.gameObject.SetActive(true);             // keeps score on the scene.


                if (Input.GetKeyDown(KeyCode.R)) {                  // Restarts the game if r is pressed.
                    Scene scene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(scene.name);
                }
                break;
        }
        if (Input.GetKeyUp(KeyCode.Escape)) {           // Exits game.
            Application.Quit();
        }

    }
    /// <summary>
    /// Checks if Players dead.
    /// </summary>
    /// <returns></returns>
    public void PlayerDead() {
        // Turns off player, un child camera as to not disable it and Enables Death Screen.
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


