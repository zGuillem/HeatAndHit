using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lifeCounter : MonoBehaviour
{
    public GameObject markObject;

    public GameObject prefabLifeBar;

    public int lifeBarSize = 10;
    public int sepUnder = 2;
    public int marcSize = 2;

    private List<GameObject> lifeBars = new List<GameObject>();

    public void maxLife(int value, int actualValue)
    {
        //Delete list
        foreach (GameObject obj in lifeBars)
        {
            Destroy(obj, 0f);
        }

        //Generate new list
        for (int i = 0; i < value; i++)
        {
            GameObject g = Instantiate(prefabLifeBar, transform);
            g.transform.localPosition = new Vector3(0f, i * 46 + 2, 0f);
            g.transform.localRotation = Quaternion.identity;

            lifeBars.Add(g);
        }

        //Adapt mark

        markObject.GetComponent<Image>().rectTransform.sizeDelta = new Vector2 (32f, value * 12 + 4);

        //Actualitzar vida
        actualLife(actualValue);
    }

    public void actualLife(int value)
    {
        int counter = 0;
        foreach(GameObject obj in lifeBars)
        {
            obj.GetComponent<lifeBar>().activar(counter < value);
            counter++;
        }
    }
}
