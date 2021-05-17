using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    // Cameras.

    public Camera m_MainCamera;
    public Camera m_IntroCamera;
    
    
    // Text.
    public TextMeshProUGUI m_ScoreText;
    
    
    
    
    // GameObjects.
    public GameObject m_Player;         // Player Gameobject

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
        m_GameState = GameState.Start;

        m_MainCamera.enabled = false;
        m_ScoreText.gameObject.SetActive(false);   // Sets Score Text to off. 
        
    }

    private void Update() {
        switch (m_GameState) {
            case GameState.Start:
                if (Input.GetKeyDown(KeyCode.Return) == true) {         // If player hits enter.
                    m_MainCamera.enabled = true;
                    m_IntroCamera.enabled = false;
                    Debug.Log("Playing");
                    m_GameState = GameState.Playing;                 // Sets State to Playing.
                    
                }
                break;
            case GameState.Playing:
                m_ScoreText.gameObject.SetActive(true);

                //if (IsPlayerDead() == true) {       // Checks if Player is dead.
                 //   m_GameState = GameState.GameOver;
                //}
                break;
            case GameState.GameOver:
                if (Input.GetKeyUp(KeyCode.R)) {
                    // ToDo Restart Game.
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
    private bool IsPlayerDead() {
        if (m_Player.activeSelf == false) {
            return true;
        }
        return false;
    }

}

