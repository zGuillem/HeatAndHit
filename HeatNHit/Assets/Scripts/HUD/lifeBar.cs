using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class lifeBar : MonoBehaviour
{
    public GameObject plena;
    public GameObject buida;

    public void activar(bool activada)
    {
        plena.SetActive(activada);
        buida.SetActive(!activada);
    }
}
