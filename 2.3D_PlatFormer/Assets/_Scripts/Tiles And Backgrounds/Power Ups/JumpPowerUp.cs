using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpPowerUp : MonoBehaviour {
    private PlayerController m_PlayerController;

    public float m_Seconds = 15f;                           // Power up Time.
    private bool m_TimerRunning;                           // if timer running.

    private float m_PlayerControllerJumpHeight;          
    private float m_OriginalJumpHighest;

    private Renderer m_Renderer;

    private void Start() {
        m_Renderer = GetComponent<Renderer>();
        m_TimerRunning = false;
    }
    private void OnTriggerEnter(Collider other) {
        // If Hits Player  * Jump height by 2.
        if (other.tag == "Player") {
            m_PlayerController = other.GetComponent<PlayerController>();
            m_OriginalJumpHighest = m_PlayerController.m_JumpHeight;
            m_PlayerControllerJumpHeight = m_PlayerController.m_JumpHeight * 2;
            m_TimerRunning = true;
            m_Renderer.enabled = false;
        }
    }
    private void Update() {
        // Runs timer If timerRunning = true.
        if (m_TimerRunning) { m_Seconds -= Time.smoothDeltaTime; }
        if (m_Seconds >= 0 && m_TimerRunning ) {
            m_PlayerController.m_JumpHeight = m_PlayerControllerJumpHeight;
            Debug.Log(m_Seconds);
            if (m_Seconds <= 1) { m_PlayerController.m_JumpHeight = m_OriginalJumpHighest;
                m_TimerRunning = false;
                GameObject obj = this.transform.parent.gameObject;  Destroy(obj);}
        }
    }
}
