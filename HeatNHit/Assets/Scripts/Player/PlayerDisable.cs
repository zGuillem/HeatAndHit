using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisable : MonoBehaviour
{
    public PlayerMove   playerMove;
    public GameObject[] weaponList;
    public void disableAll()
    {
        playerMove.PlayerSpeed = 0;

        for(int i = 0; i < weaponList.Length; i++)
        {
            var aux = weaponList[i].GetComponent<gunScript>();
            Debug.Assert(aux != null);
            aux.enabled = false;
        }
    }
}
