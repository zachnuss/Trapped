using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Transform moveLocation;
   // public Transform moveMid;

    public bool direction = true;

    public GameObject pos1;
    public GameObject pos2;

    //public Transform mid1;
    //public Transform mid2;

    //future idea: add one way doors that players have to manually turn on and off or switch which direcitons they can go

    private void Update()
    {
        if (direction)
        {
            moveLocation = pos1.transform;
            //moveMid = mid1;
        }
        else
        {
            moveLocation = pos2.transform;
            //moveMid = mid2;
        }
    }

    public void SwitchDirection()
    {
      //  if (direction)
     //       direction = false;
       // else
         //   direction = true;
    }
}
