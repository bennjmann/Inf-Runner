using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    private CharacterController m_CharacterController;
    public Animator m_Animator;
    public GameManager m_GameManager;
    

    public float m_Gravity = -9.81f;           // Gravity Value.

    public bool m_IsGrounded;                     // Checks if player is grounded.
    public float m_fallMultiplier = 2f;          // Used to fall faster after jumping. 
    public float m_JumpHeight = 2f;             // Jump Height.
    private bool m_Jump;                       // Jump key Input.
    
    public float m_ZMoveSpeed;                  // Default Speed.
    private Sprint m_Sprint;                   // Speed of Sprinting.
 
    public float m_XMoveSpeed = 5f;             // Vertical Speed.
    private float m_Vertical;                  // Vertical Movement Key Input.


    private Vector3 m_PlayerVelocity;          // Players Velocity (Being used for Gravity).
    
    public float m_DistanceTravelledZ;           // Distance Traveled Use for score.
    private Vector3 m_LastPos;                 // Last Position Used for score. 

    private void Awake() {
        m_CharacterController = GetComponent<CharacterController>();
        m_Sprint = GetComponent<Sprint>();
        m_GameManager = m_GameManager.GetComponent<GameManager>();
        m_Animator = GetComponent<Animator>();
        
    }

    private void Start() {
        m_ZMoveSpeed = 0f;
        m_LastPos = transform.position;       // Sets LastPos To Starting Position.
    }
    
    
    private void Update() {
        if (m_GameManager.State == GameManager.GameState.Start) {          //part of animations
            m_Animator.SetBool("isMoving", true);
        }
        Jumping();
        Inputs();
        DistanceTraveled();
        Animations();
        
        
    }

    private void FixedUpdate() {
        Movement();
    }
    
    /// <summary>
    /// Movement of Player.
    /// </summary>
    private void  Movement() {
        if (m_GameManager.State == GameManager.GameState.Start) {
            m_Vertical = 0f;
            m_ZMoveSpeed = 0f;
        }
        var move = new Vector3(-m_Vertical * m_XMoveSpeed, m_PlayerVelocity.y += m_Gravity * Time.deltaTime, m_ZMoveSpeed);       // (Input of Players Press (Vertical) * (M_XMoveSpeed), Gravity, Move's player Forwards/To Z.
        m_CharacterController.Move(move * Time.deltaTime);                                                                    // Moves Character Based on (Move) Vector.
    }

    /// <summary>
    /// Handles Jumping For the player.
    /// </summary>
    private void Jumping() {
        m_IsGrounded = m_CharacterController.isGrounded;           // Sets m_IsGrounded to CC.isGrounded Properties
        if (m_GameManager.State == GameManager.GameState.Start) {
            m_Jump = false;
        }
          if ( m_Jump && m_IsGrounded) {
            // If Key is Pressed and Grounded.
            m_PlayerVelocity.y += Mathf.Sqrt(m_JumpHeight * -3.0f * m_Gravity);                                           // Jump (Velocity.y += Sqrt JumpHeight+-)..
        }  if (m_PlayerVelocity.y < 0) {                                                                               // If player is falling.
            m_PlayerVelocity += Vector3.up * (m_Gravity * (m_fallMultiplier - 1) * Time.deltaTime);                       // Used to fall Faster by adding the fall multiplier to the downforces.
        }  if (m_IsGrounded && m_PlayerVelocity.y < 0) {                                                             // Ask if grounded and Velocity.y < 0.
              m_PlayerVelocity.y = 0f;                                                                                  // Sets Player's Velocity.y to 0 When Grounded.else 
          } 
        
    }
    /// <summary>
    /// Handles Inputs.
    /// </summary>
    private void Inputs() {
        m_Vertical = Input.GetAxis("Vertical");         // Movement Keys.
        m_Jump = Input.GetButtonDown("Jump");          // Jump Keys.
    }
/// <summary>
/// Kills Player if Controller Moves into Hit Object With Tag KillLayer.
/// </summary>
/// <param name="hit"></param>
    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.layer == LayerMask.NameToLayer("KillLayer")) {
            Destroy(gameObject);
        }
    }
    
    /// <summary>
    /// Distance Player has Traveled, Basically the score.
    /// </summary>
    private void DistanceTraveled() { 
        // Calculates Score By distance Traveled in the z.
        if (Input.GetAxisRaw("Sprint") == 1 && m_Sprint.m_IsSprinting == true) {                    
            Vector3 distanceVector = transform.position - m_LastPos;  
            float DistanceThisFrame = distanceVector.z * 2;
            m_DistanceTravelledZ += DistanceThisFrame;
            m_LastPos = transform.position;
        }
        else {
            Vector3 distanceVector = transform.position - m_LastPos;  
            float DistanceThisFrame = distanceVector.z;
            m_DistanceTravelledZ += DistanceThisFrame;
            m_LastPos = transform.position;
        }
    }
    /// <summary>
    /// If Player is Doing actions then Do Animation.
    /// </summary>
    void Animations() {
        // Start
        if (m_GameManager.State == GameManager.GameState.Playing) {
            m_Animator.SetBool("isMoving", true);
        }
        
        
        // Falling
        if (m_IsGrounded) {
            m_Animator.SetBool("isFalling", false);
        } else if (m_PlayerVelocity.y < 0) m_Animator.SetBool("isFalling", true);
        
        // Jumping
        if (m_Jump && m_IsGrounded) {
            m_Animator.SetBool("isJumping", true);
        } else m_Animator.SetBool("isJumping", false);
        
        // Sprinting
        if (m_Sprint.m_IsSprinting == true) {                               
            m_Animator.SetBool("isSprinting", true);
        } else m_Animator.SetBool("isSprinting", false);
    }
}
