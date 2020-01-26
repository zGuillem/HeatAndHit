using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mapHud : MonoBehaviour
{
    public Image agullaGran;
    public Transform agullaGranTransform;

    public Image agullaPetita;
    public Transform agullaPetitaTransform;

    public Image playerPosition;
    public Transform playerTransform;

    public float factorScale;

    // Update is called once per frame
    void Update()
    {

        //Actualitzar rotacions;
        Quaternion quaternion = new Quaternion(0f, 0f, -agullaPetitaTransform.localRotation.y, agullaPetitaTransform.localRotation.w);
        agullaPetita.transform.localRotation = quaternion;

        quaternion = new Quaternion(0f, 0f, -agullaGranTransform.localRotation.y, agullaGranTransform.localRotation.w);
        agullaGran.transform.localRotation = quaternion;

        quaternion = new Quaternion(0f, 0f, -playerTransform.localRotation.y, playerTransform.localRotation.w);
        playerPosition.transform.localRotation = quaternion;

        //Actualitzar posicio del jugador respecte el mapa
        Vector3 posEnElMapa = new Vector3(playerTransform.localPosition.x, playerTransform.localPosition.z, playerTransform.localPosition.y);
        playerPosition.transform.localPosition = posEnElMapa* factorScale;
    }
}
