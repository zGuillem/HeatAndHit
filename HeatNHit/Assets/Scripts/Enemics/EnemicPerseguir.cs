using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*************************************/
/* Script de control dels enemics,
 * Controla el moviment cap el jugador
*/
public class EnemicPerseguir : MonoBehaviour
{
    Transform target; //Objectiu dels enemics, el jugador
    void Start()
    {
        target = PlayerManager.instance.player.transform; //Seleccionem el jugador com a objectiu
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(target.position - transform.position); //Movem l'enemic en la direcció del jugador
    }

}
