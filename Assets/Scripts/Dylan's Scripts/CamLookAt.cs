using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Transform transversalTrans;

    //location of camera (will be set from door prefab)
    public Vector3 offset;
    //smoothing of camera
    public float smoothSpeed = 0.1f;

    public bool transversalSide = false;

    //interpolation to move
    private Transform c0, c1;
    Vector3 p01;
    private Quaternion r01;
    float timeDuration = 1f;
    bool checkToCalculate = false;
    bool moving = false;
    float timeStart;
    float u;

    //extrapolation
    float uMin = 0f;
    float uMax = 1;

    private EasingType easingType = EasingType.linear;
    private float easingMod = 2f;
    private bool loopMove = true;

    // fixed update for movment based
    void FixedUpdate()
    {
        //follow player

        Vector3 desiredPosition = playerTarget.localPosition + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.localPosition, desiredPosition, smoothSpeed);
        transform.localPosition = smoothedPosition;

    }

    public void BeginSideTransversal(Transform newTrans)
    {
        transversalTrans = newTrans;
        transversalSide = true;
        checkToCalculate = true;
    }

    //when player moves to next side, we set the orientation of camera to be the same forward as the player's forward
    void SideTransversal1()
    {
        if (checkToCalculate)
        {
            Debug.Log("begine interpolation (with easing)");
            checkToCalculate = false;
            moving = true;
            timeStart = Time.time;
            c0 = this.transform;
            c1 = transversalTrans;

            //StartCoroutine(transversalTimer());
        }

        if (moving)
        {
            u = (Time.time - timeStart) / timeDuration;


            // if(u >= 1)
            // {
                //stop if we go greater than 100%
               // Debug.Log("Eached new pos");
               // u = uMax;
               // moving = false;
             //}

            // if(u <= uMin)
            // {
            //    Debug.Log("reached new pos2");
             //    u = uMin;
              //   moving = false;
             //}

            if (loopMove)
            {
                timeStart = Time.time;
            }
            else
            {
                moving = false;
            }

            //adjsut u value to the ranger from uMin to uMax
            //different types of eases to avoid snaps and rigidness
            u = EaseU(u, easingType, easingMod);
            //interpolation equation (with min and max)
            //u = ((1 - u) * uMin) + (u * uMax);

            //standard linear inter
            //position
            p01 = (1 - u) * c0.position + u * c1.position;

            //quaternions are different
            //use unities built in spherical linear interpolation
            //SLERP
            r01 = Quaternion.Slerp(c0.rotation, c1.rotation, u);


            //apply those new values
            transform.position = p01;
            //this.GetComponent<Renderer>().material.color = c01;
            //transform.localScale = s01;
            transform.rotation = r01;
        }

    }

    //bezier
    void SideTransversal2()
    {
        //
    }

    // to ensure that the camera goes the same spot and not a near appox, after the given time, we snap the camera to the target pos. Its so small its unnoticable
    IEnumerator transversalTimer()
    {
        yield return new WaitForSeconds(timeDuration);
        moving = false;
        //move camera to transverseTrans
        this.transform.position = transversalTrans.position;
        transversalSide = false;
    }

    //tab twice for it to automake statement
    public float EaseU(float u, EasingType eType, float eMod)
    {
        float u2 = u;
        switch (eType)
        {
            case EasingType.linear:
                u2 = u;
                break;
            case EasingType.easeIn:
                u2 = Mathf.Pow(u, eMod);
                break;
            case EasingType.easeOut:
                u2 = 1 - Mathf.Pow(1 - u, eMod);
                break;
            case EasingType.easeInOut:
                if (u <= 0.5f)
                {
                    u2 = 0.5f * Mathf.Pow(u * 2, eMod);
                }
                else
                {
                    u2 = 0.5f + 0.5f * (1 - Mathf.Pow(1 - (2 * (u - 0.5f)), eMod));
                }
                break;
            case EasingType.sin:
                u2 = u + eMod * Mathf.Sin(2 * Mathf.PI * u);
                break;
            case EasingType.sinIn:
                u2 = 1 - Mathf.Cos(u * Mathf.PI * 0.5f);
                break;
            case EasingType.sinOut:
                u2 = Mathf.Sin(u * Mathf.PI * 0.5f);
                break;
            default:
                break;
        }

        return (u2);
    }

    //most likely will use lerp for smooth linear interpolation and slerp for rotation
}
