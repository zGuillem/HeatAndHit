using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    //VARIABLES -----------------------------------------------------------------------------------------------------------

    //MOVIMENT ----------------------------
    public string NomInputHoritzontal;
    public string NomInputVertical;
    public float VelocitatMoviment;

    //SALT --------------------------------
    public AnimationCurve Salt;
    public float MultiplicadorSalt;
    public KeyCode TeclaSalt;

    private bool estaSaltant;

    private CharacterController ControlPersonatje;

    //MÈTODES PRIVATS ------------------------------------------------------------------------------------------------------

    private void Start() //Es crida al començament del joc i inicialitza el jugador;
    {
        ControlPersonatje = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MovimentJugador();
    }

    private void MovimentJugador()
    {
        float InputHoritzontal = Input.GetAxis(NomInputHoritzontal) * VelocitatMoviment;
        float InputVertical = Input.GetAxis(NomInputVertical) * VelocitatMoviment;

        Vector3 MovimentEndavant = transform.forward * InputVertical;
        Vector3 MovimentDreta = transform.right * InputHoritzontal;

        ControlPersonatje.SimpleMove(MovimentEndavant + MovimentDreta);
        //transform.position += ((MovimentEndavant + MovimentDreta) * Time.deltaTime * VelocitatMoviment);
        InputSalt();

    }

    private void InputSalt()
    {
        if (Input.GetKeyDown(TeclaSalt) && !estaSaltant)
        {
            estaSaltant = true;
            StartCoroutine(EventSalt());
        }
    }

    private IEnumerator EventSalt()
    {

        ControlPersonatje.slopeLimit = 90.0f;
        float TempsenAire = 0.0f;

        do
        {
            float ForcaSalt = Salt.Evaluate(TempsenAire);
            ControlPersonatje.Move(Vector3.up * ForcaSalt * MultiplicadorSalt * Time.deltaTime);
            //transform.position += (Vector3.up * ForcaSalt * MultiplicadorSalt * Time.deltaTime);
            TempsenAire += Time.deltaTime;
            yield return null;
        } while (!ControlPersonatje.isGrounded && ControlPersonatje.collisionFlags != CollisionFlags.Above);

        ControlPersonatje.slopeLimit = 45.0f;
        estaSaltant = false;

    }

}
