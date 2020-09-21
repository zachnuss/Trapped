using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleBool : MonoBehaviour
{
    public bool active = false;
    public Material pressed;


   public void onPress()
    {
        gameObject.GetComponent<Renderer>().material = pressed;


    }



}
