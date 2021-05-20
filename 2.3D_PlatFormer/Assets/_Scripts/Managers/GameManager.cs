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

    
    // Other Managers.
    public GameObject m_LevelManager;
    
    /// <summary>
    /// States of the game.
    /// </summary>
    public enum GameState {         
        MainMenu,
        Start,
        Playing,
        GameOver
    };
    private GameState m_GameState;
    public GameState State { get { return m_GameState; } }          // Displays State On Unity.




    private void Awake() {
        m_GameState = GameState.MainMenu;
        m_Player.SetActive(false);
        m_LevelManager.SetActive(false);
        m_MainCamera.enabled = false;
        m_ScoreText.gameObject.SetActive(false);   // Sets Score Text to off. 
        
        
    }

    private void Update() {
        switch (m_GameState) {
            case GameState.MainMenu:
                if (Input.GetKeyDown(KeyCode.Return) == true) {
                    m_GameState = GameState.Start;
                    
                }
            break;
            case GameState.Start:
                if (Input.GetKeyDown(KeyCode.Return) == true) {         // If player hits enter.
                    m_Player.SetActive(true);
                    m_LevelManager.SetActive(true);
                    m_MainCamera.enabled = true;
                    m_IntroCamera.enabled = false;
                    Debug.Log("Playing");
                    m_GameState = GameState.Playing;                 // Sets State to Playing.
                    
                }
                break;
            case GameState.Playing:
                m_ScoreText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.End)) {
                m_GameState = GameState.GameOver;
            }
                
                break;
            case GameState.GameOver:
                if (Input.GetKeyUp(KeyCode.R)) {
                    // ToDo Restart Game.
                    m_GameState = GameState.Start;
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

