using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLook : MonoBehaviour
{

    //VARIABLES -----------------------------------------------------------------------------------------------------------

    public string inputXRatoli;          //Guarda l'input de l'eix X del ratolí.
    public string inputYRatoli;          //Guarda l'input de l'eix Y del ratolí.
    public float sensibilitatRatoli;     //Guarda el valor de la sensibilitat que s'assigna al ratolí.
    public Transform cosJugador;

    private float limitEix_X;

    //MÈTODES PRIVATS ------------------------------------------------------------------------------------------------------

    private void Start() //Es crida al començament del joc i activa la camera posant-la al centre.
    {
        FixarCursor();
        limitEix_X = 0.0f;
    }

    private void FixarCursor() //Es cridarà sempre que es vulgui fixar el cursor al centre de la pantalla.
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void DesfixarCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void RotacioCamera() //Gestiona la rotació de la camara per mitjà de l'input del ratolí.
    {
        float ratoliX = Input.GetAxis(inputXRatoli) * sensibilitatRatoli * Time.deltaTime;
        float ratoliY = Input.GetAxis(inputYRatoli) * sensibilitatRatoli * Time.deltaTime;

        limitEix_X += ratoliY;

        if (limitEix_X > 90.0f)
        {
            limitEix_X = 90.0f;
            ratoliY = 0.0f;
            LimitEix_XaValor(270.0f);
        }
        else if (limitEix_X < -90.0f)
        {
            limitEix_X = -90.0f;
            ratoliY = 0.0f;
            LimitEix_XaValor(90.0f);
        }

        transform.Rotate(Vector3.left * ratoliY);
        cosJugador.Rotate(Vector3.up * ratoliX);
    }

    public void Recoil(float s)
    {
        limitEix_X += s;
        transform.Rotate(Vector3.left * s);
    }

    private void Update()  // Actualitza la càmera.
    {
        RotacioCamera();
    }

    private void LimitEix_XaValor(float valor) //Assigna un valor al limit de l'eix x de la càmara per evitar que es pugui donar la volta sencera.
    {
        Vector3 rotacio_euler = transform.eulerAngles;
        rotacio_euler.x = valor;
        transform.eulerAngles = rotacio_euler;
    }

}
