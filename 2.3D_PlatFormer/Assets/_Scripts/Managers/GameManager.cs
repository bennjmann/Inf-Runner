using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instantiate;
    private bool m_IsinstantiateNotNull;

    public Scene m_CurrentScene;
    
    private void Start() {
        m_IsinstantiateNotNull = instantiate != null;
    }
    private void Update() {
        m_CurrentScene = SceneManager.GetActiveScene();
        



        if (m_IsinstantiateNotNull) {
            Destroy(gameObject);
        } else { instantiate = this; DontDestroyOnLoad(gameObject); }
    }
    
    public void LoadScene() { 
        if (m_CurrentScene.name == "MainMenu") {
        SceneManager.LoadScene("GamePlay");
    }}
}
