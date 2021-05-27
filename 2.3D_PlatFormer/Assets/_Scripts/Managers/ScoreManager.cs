using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
   public TextMeshProUGUI m_Score;             // Score Text.
   private GameObject m_Player;    // Used to check if player is alive.

   public PlayerController m_CurrentScore;      // This is DistanceTravelled From player Controller used as score.
   public GameManager m_GameManager;

   private void Start() {
      m_GameManager = m_GameManager.GetComponent<GameManager>();
   }
   private void Update() {
       m_Score.text = "Score:" + (int)m_CurrentScore.m_DistanceTravelledZ; // Updated Text To display Score.
   }



}
