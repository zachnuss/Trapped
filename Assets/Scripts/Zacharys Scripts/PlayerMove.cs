using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public enum currentAxis
{
    x,
    y,
    z,
}
public class PlayerMove : MonoBehaviour
{
    public currentAxis axis;
   // private float xMove, zMove;
    Vector2 movementInput;
    public float movementSpeed;
    public float rotTime;
    public Vector3 endRot;
    float _angle;
    float startTime;

    bool timeStart;
    bool rotate;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!DetectEdge()&&!rotate)
        {
            Movement();
            Debug.Log(transform.forward);
        }
        if (DetectEdge()&& !rotate)
        {
            findDirectionFacing();

        }
        if (rotate)
        {
            rotationLerp(startTime, endRot);
        }
        
    }


    private void OnMove1(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }
    //joysticks
    public float JoystickH()
    {
        //float r = 0.0f;
        float h = movementInput.x;
        //r += Mathf.Round(h);
        return Mathf.Clamp(h, -1.0f, 1.0f);
    }
    public float JoystickV()
    {
        //float r = 0.0f;
        float v = movementInput.y;
        //r += Mathf.Round(v);
        return Mathf.Clamp(v, -1.0f, 1.0f);
    }
    public Vector3 MainJoystick()
    {
        return new Vector3(JoystickH(), 0, JoystickV());
    }
    void RotateMovement(Vector3 movement)
    {
        //convert joystick movements to angles that we can apply to player rotation
        _angle = Mathf.Atan2(movement.x, movement.z);
        _angle = Mathf.Rad2Deg * _angle;

        //local angles are used since its a child, the player parent is set to keep track of the global rotation
        transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, _angle, transform.localEulerAngles.z);


        // Debug.Log(transform.localEulerAngles.x + _angle + transform.localEulerAngles.z);

        //transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, _angle);

        //transform.localRotation.SetEulerAngles(transform.localEulerAngles.x, _angle, transform.localEulerAngles.z);
        //transform.eulerAngles.y = _angle;
        //transform.rotation = Quaternion.Euler(0, -_angle, 0);
        //Debug.Log(transform.localRotation);
        //player is always moving forward, player is just adjsuting which way they move forward (always local forward so we can have player move consistentaly forward on each side)
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }

    void Movement()
    {
        Vector3 movement = MainJoystick();
        //Debug.Log(movement);
        //only move if player gives input
        if (movement != Vector3.zero)
            RotateMovement(movement);

    }



    bool DetectEdge()
    {
        bool noFloor = false;
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down), Color.green);

        int layerMask = 1 << 8;
        //everything but 8
        layerMask = ~layerMask;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down) * 6, out hit, Mathf.Infinity, layerMask))
        {
            //if we dont hit anything, char is hanging over edge
            //if(hit.collider.tag != "Cube")
            noFloor = false;
        }
        else
            noFloor = true;

        return noFloor;
    }
    void findDirectionFacing()
    {
        float x = Mathf.Abs(transform.forward.x);
        float z = Mathf.Abs(transform.forward.z);
        float y = Mathf.Abs(transform.forward.y);
        float startTime = Time.time;
        switch (axis)
        {
            case currentAxis.x:
                compareAxisValues(y, z,transform.forward.y, transform.forward.z);
                break;
            case currentAxis.y:
                compareAxisValues(x,z, transform.forward.x, transform.forward.z);
                break;
            case currentAxis.z:
                compareAxisValues(x, y,transform.forward.x, transform.forward.y);
                break;
            default:
                break;
        }
       
        


    }
    void compareAxisValues(float value1,float value2,float transform1,float transform2)
    {
        if (value1 > value2)
        {
            if (transform1 > 0)
            {
                if(axis == currentAxis.y)
                {
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    endRot = transform.rotation.eulerAngles + new Vector3(90, 0, 0);
                    rotate = true;

                }
                //rotate -z
            }
            if (transform1 < 0)
            {
                if (axis == currentAxis.y)
                {
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    endRot = transform.rotation.eulerAngles + new Vector3(-90, 0, 0);
                    rotate = true;

                }
            }


        }
        if (value2 > value1)
        {
            if (transform2 > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (transform2 < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);

            }

        }


    }
    void rotationLerp( float startTime, Vector3 endRot)
    {
        
        if (timeStart)
        {
            Debug.Log("start rot");
            startTime = Time.time;
            timeStart = false;
        }
        float u = (Time.time - startTime)/rotTime;
        if (u >= 1)
        {
            timeStart = true;
            rotate = false;
            
        }
        Vector3 p1;
        p1 = (1 - u) * transform.rotation.eulerAngles + u * endRot;
        transform.rotation = Quaternion.Euler(p1);
    }
    void movementLerp()
    {




    }


}
