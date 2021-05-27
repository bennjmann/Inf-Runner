using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Scene m_CurrentScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_CurrentScene = SceneManager.GetActiveScene(); // Get current scene 
    }

    public void Quit() {
        Application.Quit();
    }
    public void LoadScene() {
        if (m_CurrentScene.name == "MainMenu") {
            SceneManager.LoadScene("GamePlay");
        }}
}
