using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; // Required when Using UI elements.
using UnityEngine;

//Author: Oriol Perarnau Arnau

[ExecuteInEditMode] //See changes on editor

public class placeHud : MonoBehaviour
{
    //Class to place an image on the hud evading screen size issues

    public enum borderVertical
    {
        Up,
        Down
    }

    public enum borderHorizontal
    {
        Left,
        Right
    }

    public borderVertical vertical;                 //Which side on the vertical axis use as anchor
    public borderHorizontal horizontal;             //Which side on the horizontal axis use as anchor


    public float pos_x;                             //Position in X that the image must be from anchor
    public float pos_y;                             //Position in Y that the image must be from anchor

    public RectTransform objectToMove;              //Reference to the RectTransform of the object

    void Start()
    {
        //Calculate screen size using of reference the center of the screen
        float width = Screen.width/2;    
        float height = Screen.height/2;

        float newX = 0;
        float newY = 0;

        switch (horizontal)     //Calculate new position in anchor
        {
            case borderHorizontal.Right:
                newX = width - pos_x;
                break;
            case borderHorizontal.Left:
                newX = -width + pos_x;
                break;
        }

        switch (vertical)       //Calculate new position in anchor
        {
            case borderVertical.Up:
                newY = height - pos_y;
                break;
            case borderVertical.Down:
                newY = -height + pos_y;
                break;
        }

        //Change position
        objectToMove.anchoredPosition = new Vector2(newX, newY);
    }
}
