using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    //VARIABLES -----------------------------------------------------------------------------------------------------------
    public CharacterController characterController;
    public PlayerFeedback feedback;


    //MOVIMENT ----------------------------
    public float PlayerSpeed = 5.0f;
    public float JumpSpeed = 5.0f;
    public float gravity = -10f;

    private float Speed = 0f;

    private bool m_Grounded;
    private float m_GroundedTimer;
    private float m_SpeedAtJump = 0.0f;
    private float m_VerticalSpeed = 0.0f;

    //MÈTODES PRIVATS ------------------------------------------------------------------------------------------------------

    private void Start() //Es crida al començament del joc i inicialitza el jugador;
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        bool loosedGrounding = false;
        //S'utilitza el character controller amb timer per evitar falsos positius
        if (!characterController.isGrounded)
        {
            if (m_Grounded)
            {
                m_GroundedTimer += Time.deltaTime;
                if (m_GroundedTimer >= 0.5f)
                {
                    loosedGrounding = true;
                    m_Grounded = false;
                }
            }
        }
        else
        {
            m_GroundedTimer = 0.0f;
            m_Grounded = true;
        }

        Speed = 0;
        Vector3 move = Vector3.zero;

        if (m_Grounded && Input.GetButtonDown("Jump"))
        {
            m_VerticalSpeed = JumpSpeed;
            m_Grounded = false;
            loosedGrounding = true;
        }

        if(m_VerticalSpeed > 0 && Input.GetButtonUp("Jump"))
        {
            m_VerticalSpeed *= 0.5f;
        }


        if (loosedGrounding)
        {
            m_SpeedAtJump = PlayerSpeed;
        }

        // Move around with WASD
        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (move.sqrMagnitude > 1.0f)
            move.Normalize();

        float usedSpeed = m_Grounded ? PlayerSpeed : m_SpeedAtJump;
        //                condition  ? consequent  : alternative  ;

        move = move * usedSpeed * Time.deltaTime;

        move = transform.TransformDirection(move);
        characterController.Move(move);

        Speed = move.magnitude / (PlayerSpeed * Time.deltaTime);

        // Fall down / gravity
        m_VerticalSpeed += gravity * Time.deltaTime;
        if (m_VerticalSpeed < gravity)
            m_VerticalSpeed = gravity; // max fall speed
        var verticalMove = new Vector3(0, m_VerticalSpeed * Time.deltaTime, 0);
        var flag = characterController.Move(verticalMove);
        if ((flag & CollisionFlags.Below) != 0)
            m_VerticalSpeed = 0;
    }

}
