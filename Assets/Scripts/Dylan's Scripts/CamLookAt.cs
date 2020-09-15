using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dylan Loe
/// 
/// Cam movement system
/// 
/// NOT IN USE
/// </summary>


public enum EasingType
{
    linear,
    easeIn,
    easeOut,
    easeInOut,
    sin,
    sinIn,
    sinOut
}

public class CamLookAt : MonoBehaviour
{
    //basic camera looking at player (will be updated later)

    /**
     *  Notes:
     *  
     *  when player rotates to next side, camera will need to change orientation
     *      - this is determined based off of the door player interacts with to move
     *          - Each door will have a preassigned orientation (each door has two directions players can go, therefore two locations for camera
     *      - to move camera we will use XXX_TBD_XXX
     *          - there are some camera move plug ins to check out
     *          - camera movements need to be smooth - cant disorentate player
     *      - camera angle cannot hid things behind walls and must show player map to a readable and transverable extent
     *      - Player cant be confused when moving to next 'room' (wall)
     *      
     *      -when player beats objective, there must be some way of them moving to next cube?
     * 
     */

    //
    public Transform playerTarget;

    //location of camera (will be set from door prefab)
    public Vector3 offset;
    //smoothing of camera
    public float smoothSpeed = 0.1f;

    //public bool transversalSide = false;
   

    // fixed update for movment based
    void FixedUpdate()
    {
        //follow player

        Vector3 desiredPosition = playerTarget.localPosition + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.localPosition, desiredPosition, smoothSpeed);
        transform.localPosition = smoothedPosition;

    }

}
