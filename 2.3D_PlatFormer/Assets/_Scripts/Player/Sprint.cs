using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Sprint : MonoBehaviour {
    public PlayerController m_PlayerSpeed;                       // Players Current Speed.
    public UnityEngine.UI.Slider m_Slider;                     // Sprint Slider its self
    
    private float m_OriginalXSpeed; 
    public float m_BaseZMoveSpeed = 5.5f;                   // Base Speed Moving Right (Should be 10 less then ZMoveSpeed when sprinting).
    
    [SerializeField] 
    private float m_ZWithSprintSpeed;                   // Base speed + sprint Max.
    [SerializeField]
    float m_NewPlayerXSpeed;                          // Base speed of x / 2. 
    
    public float m_SprintGain = 2.5f;
    public float m_MaxSprint = 10f;                // Max Speed of the sprint.
    public float m_ZSprintAccUP;                   // Speed At which you gain Acc To max.
    public float m_ZSprintAccDown;                // Speed At which you Lose Acc To min/Base.
    public float m_ZSpeedGain = 2f;                  // The Gain at which X Gains Ever time score is  a Division 100
    public float m_XSpeedGain = 1f;                 // The Gain at which X Gains Ever time score is  a Division 100
    public float m_XSpeedLost = 1.3f;              // The Amount of speed lost in the x when sprinting in the z.


    private float m_Sprint;                     // Sprint Key Input.
    
    private bool SpeedControl;              // used so it doesnt add speed more the once when needed.
    public bool m_IsSprinting;             // Check if Sprinting.    

    private void Start() {
        m_IsSprinting = false;
        m_Slider =  m_Slider.GetComponent<UnityEngine.UI.Slider>();        // Grabs slider from m_sprintSlider.
        m_PlayerSpeed = GetComponent<PlayerController>();                       // Grabs the Float from PlayerController script.
        m_Slider.maxValue = m_MaxSprint;
        // Sets the Base speeds.
        m_PlayerSpeed.m_ZMoveSpeed = m_BaseZMoveSpeed;        
        m_OriginalXSpeed = m_PlayerSpeed.m_XMoveSpeed;
        
    }

    private void FixedUpdate() {
        
        m_ZWithSprintSpeed = m_BaseZMoveSpeed + m_MaxSprint;                                       // Sets the max Sprint Speed.

        // If Sprint Buttons Down and Current speed is not = Max sprint speed.
        if (m_Sprint > 0 && m_PlayerSpeed.m_ZMoveSpeed < m_ZWithSprintSpeed && m_Slider.value < m_Slider.maxValue) {  
            m_PlayerSpeed.m_ZMoveSpeed += m_ZSprintAccUP * Time.smoothDeltaTime;                                 //  Z Acceleration.
            m_PlayerSpeed.m_XMoveSpeed -= m_BaseZMoveSpeed * m_XSpeedLost * Time.smoothDeltaTime;               // X Deceleration     

        } else if (m_Sprint == 1) {                                                                      
            m_IsSprinting = true;
        } if (m_Sprint == 0 && m_PlayerSpeed.m_ZMoveSpeed > m_BaseZMoveSpeed) {        
            m_PlayerSpeed.m_XMoveSpeed += m_BaseZMoveSpeed * m_XSpeedLost * Time.smoothDeltaTime;  // X Acceleration
            m_PlayerSpeed.m_ZMoveSpeed -= m_ZSprintAccDown * Time.smoothDeltaTime;                 // Z Deceleration.
            m_IsSprinting = false;
        }
        else if (m_Slider.value == m_Slider.minValue && m_Sprint == 0) {
            m_PlayerSpeed.m_XMoveSpeed = m_OriginalXSpeed;                      // if not pressing sprint set to the base speed;
            m_PlayerSpeed.m_ZMoveSpeed = m_BaseZMoveSpeed;
        }

        m_Slider.value = m_PlayerSpeed.m_ZMoveSpeed - m_BaseZMoveSpeed;             // slider shows sprint.
        
        
        // Increases the speed if Score = a Division of 100 That = 0 ( Speed controller stops from Appending more then once due to Converting a float to a int).
         if (((int)m_PlayerSpeed.m_DistanceTravelledZ % 100) == 0 && m_PlayerSpeed.m_DistanceTravelledZ != 0 && SpeedControl == true) {
             m_MaxSprint += m_SprintGain;
             m_BaseZMoveSpeed += m_ZSpeedGain;
             m_PlayerSpeed.m_ZMoveSpeed += m_ZSpeedGain;
             m_PlayerSpeed.m_XMoveSpeed += m_XSpeedGain;
             m_OriginalXSpeed += m_XSpeedGain;
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
