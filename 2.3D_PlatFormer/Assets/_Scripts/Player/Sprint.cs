using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : MonoBehaviour {
    public PlayerController m_PlayerSpeed;                       // Players Current Speed.
    
    public float m_BaseZMoveSpeed = 5.5f;            // Base Speed Moving Right (Should be 10 less then ZMoveSpeed when sprinting).
    public float m_ZSprintSpeed;                   // Base speed + sprint Max.
    
    
    public float m_MaxSprint = 10f;                // Max Speed of the sprint.
    public float m_SprintAccUP;                   // Speed At which you gain Acc To max.
    public float m_SprintAccDown;                // Speed At which you Lose Acc To min/Base.
    private float m_Sprint;                     // Sprint Key Input.
    
    public bool m_IsSprinting;
    private bool SpeedControl;
    


    private void Start() {
        m_IsSprinting = false;
        m_PlayerSpeed = GetComponent<PlayerController>();      // Grabs the Float from PlayerController script.
        m_PlayerSpeed.m_ZMoveSpeed = m_BaseZMoveSpeed;
    }
    

    private void FixedUpdate() {
        m_ZSprintSpeed = m_BaseZMoveSpeed + m_MaxSprint;                                       // Sets the max Sprint Speed.
        
        if (m_Sprint > 0 && m_PlayerSpeed.m_ZMoveSpeed < m_ZSprintSpeed) {                                 // If Sprint Buttons Down and Current speed is not = Max sprint speed.
            m_PlayerSpeed.m_ZMoveSpeed += m_SprintAccUP * Time.deltaTime;                                 //  Accelerate.
        } else if (m_Sprint == 1) {                                                                      // If Only sprint Key is being held.
            m_PlayerSpeed.m_ZMoveSpeed = m_PlayerSpeed.m_ZMoveSpeed;                                    // Set speed to Self (So That player can sprint When held).
            m_IsSprinting = true;
        } else if (m_Sprint == 0 && m_PlayerSpeed.m_ZMoveSpeed >= m_BaseZMoveSpeed) { // If Player is not Holding Key down and Current speed is over Base Speed.
            m_PlayerSpeed.m_ZMoveSpeed -= m_SprintAccDown * Time.deltaTime;   // Slowly DeAccelerate.
            m_IsSprinting = false;
        }
        // Increases the speed if Score = a Division of 100( Speed controller stops from Appending more then once due to Converting a float to a int).
        if (((int)m_PlayerSpeed.m_DistanceTravelledZ % 100) == 0 && m_PlayerSpeed.m_DistanceTravelledZ != 0 && SpeedControl == true) {
            m_BaseZMoveSpeed += 0.5f;
            m_PlayerSpeed.m_ZMoveSpeed += 0.5f;
            m_PlayerSpeed.m_XMoveSpeed += 0.25f;
            SpeedControl = false;
        } 
    }

    private void Update() {
        if (((int)m_PlayerSpeed.m_DistanceTravelledZ % 100) != 0) {     // So speed Updates once.
            SpeedControl = true;
        }
        
        
        m_Sprint = Input.GetAxis("Sprint");                                      // Sprint Keys.
    }
}
