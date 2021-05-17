using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : MonoBehaviour {
    public float m_PlayerSpeed;                       // Players Current Speed.
    public float m_BaseZMoveSpeed = 5.5f;            // Base Speed Moving Right (Should be 10 less then ZMoveSpeed when sprinting).
    private float m_ZSprintSpeed;                   // Base speed + sprint Max.
    public float m_MaxSprint = 10f;                // Max Speed of the sprint.
    public float m_SprintAccUP;                   // Speed At which you gain Acc To max.
    public float m_SprintAccDown;                // Speed At which you Lose Acc To min/Base.
    [HideInInspector]
    public float m_Sprint;                     // Sprint Key Input.
    public bool m_IsSprinting;
    


    private void Start() {
        m_IsSprinting = false;
        m_PlayerSpeed = GetComponent<PlayerController>().m_ZMoveSpeed;      // Grabs the Float from PlayerController script.
    }

    private void FixedUpdate() {
        m_ZSprintSpeed = m_BaseZMoveSpeed + m_MaxSprint;                                       // Sets the max Sprint Speed.
        if (m_Sprint == 1 && m_PlayerSpeed < m_ZSprintSpeed) {                                // If Sprint Buttons Down and Current speed is not = Max sprint speed.
            m_PlayerSpeed += m_SprintAccUP * Time.deltaTime;                                 //  Accelerate.
        } else if (m_Sprint == 1) {                                                        // If Only sprint Key is being held.
            m_PlayerSpeed = m_PlayerSpeed;                                                // Set speed to Self (So That player can sprint When held).
            m_IsSprinting = true;
        } else if (m_Sprint == 0 && m_PlayerSpeed != m_BaseZMoveSpeed) { // If Player is not Holding Key down and Current speed is over Base Speed.
            m_PlayerSpeed -= m_SprintAccDown * Time.deltaTime; // Slowly DeAccelerate.
            m_IsSprinting = false;
        }
    }

    private void Update() {
        m_Sprint = Input.GetAxis("Sprint");                                      // Sprint Keys.
    }
}
