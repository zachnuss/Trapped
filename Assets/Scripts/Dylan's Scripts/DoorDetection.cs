using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDetection : MonoBehaviour
{
    public DoorTrigger parent;

    /// <summary>
    /// FUTURE IMPLEMENTATION
    /// 
    /// HAVE THESE DETECTIONS SET WHICH AXIS IS LOCKED
    /// </summary>

    //trans1 = true, etc
    public bool trans = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !other.GetComponent<PlayerMovement>().checkToCalculate && !other.GetComponent<PlayerMovement>().moving)
        {
            //Debug.Log("hit");
            if (trans)
            {
                //Debug.Log("setting true");
                parent.direction = true;
            }
            else
            {
               // Debug.Log("Setting false");
                parent.direction = false;
            }
        }
    }
}
