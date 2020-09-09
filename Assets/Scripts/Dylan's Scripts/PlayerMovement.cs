using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;


/// <summary>
/// Dylan Loe
/// 
/// Basic movement system
/// 
/// Notes:
/// - 6 has 6 faces, each face will therefore incorperate different downward pull on player
/// - each side therefore has different axis for movement
/// - will keep track of current face with enum
/// 
/// - regardless which way player is faceing, w is forward, s is back, a is left and d is right
/// </summary>

    //not in use
public enum faceStatus
{
    none,
    face1,
    face2,
    face3,
    face4,
    face5,
    face6
}


public class PlayerMovement : MonoBehaviour
{
    public GameObject parent;

    //PlayerInputActions controls;
    Vector3 _playerAngle;

    //tells current face player is resting on
    public faceStatus playerFaceStatus;

    public float movementSpeed = 1.0f;
    Vector2 movementInput;
    //rotation
    float _turnSpeed = 20f;
    float _angle;
    Quaternion targetRotation;

    //Camera
    public Camera playerCam;

    //when we have successfully rotated
    bool _rotating = false;
    bool _first = true;
    public bool overTheEdge = false;
    bool onDoor = false;

    //when we hit a door the player rotates and moves to this transform taken from the door prefab
    public Transform _rotationTrans;

    /// <summary>
    /// Interpolation with sin easing, most of these will be private
    /// </summary>
    private Transform c0, c1, c2, c3;
    private float timeDuration = 1f;
    public bool checkToCalculate = false;

    private Vector3 p01;
    //public Color c01;
    private Quaternion r01;
    //public Vector3 s01;

    private bool moving = false;
    private float timeStart;

    //extrapolation
    private float uMin = 0f;
    private float uMax = 1;

    //extrapolation lecture vars
    private float easingMod = 2f;
    private bool loopMove = true;

    private float u;

    //awake
    private void Awake()
    {
        //controls = new PlayerInputActions();
        Vector3 _playerAngle = Vector3.zero;
        // _playerAngle.x = 90;
    }

    // Start is called before the first frame update
    void Start()
    {
        //playerFaceStatus = faceStatus.face1;
    }

    // Used for physics 
    void FixedUpdate()
    {
        //cant move if we are rotating
        if(!overTheEdge)
            Movement();
    }

    // used for updating values and variables
    private void Update()
    {

        if (DetectEdge())
        {
            overTheEdge = true;
        }
 
        if(_rotationTrans != null)
        {
            Interpolation();
        }

        //Interpolation stuff, for rotation onto next side



        if (overTheEdge && onDoor && !checkToCalculate && !moving)
        {
            Debug.Log("got trans, start interpolation");
            //if we hit the door and are off the cube
            checkToCalculate = true;
        }
    }

    void Interpolation()
    {
        if (checkToCalculate)
        {
            Debug.Log("Moving to next side YEET");
            c0 = this.transform;
            c1 = _rotationTrans;
            checkToCalculate = false;
            moving = true;
            timeStart = Time.time;
        }

        if (moving)
        {
            Debug.Log("moving");
            u = (Time.time - timeStart) / timeDuration;

            if(u >= 1)
            {
                //when we reach new pos
                parent.transform.rotation = _rotationTrans.transform.rotation;
                u = 1;
                moving = false;
                _rotationTrans = null;
               overTheEdge = false;
                Debug.Log("IT REACHED THE END HOLY CRAP IT WORKED IMMA SLEEP NOW GGS");
            }


          //  if (loopMove)
          // // {
          //      timeStart = Time.time;
          //  }
          //  else
          //  {
          //      moving = false;
          //  }

            //adjsut u value to the ranger from uMin to uMax
            //different types of eases to avoid snaps and rigidness
            //u = u + easingMod * Mathf.Sin(2 * Mathf.PI * u);
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
            transform.rotation = r01;
        }
    }

    void Bezier()
    {
        if (checkToCalculate)
        {
            checkToCalculate = false;
            moving = true;
            timeStart = Time.time;
        }

        if (moving)
        {
            u = (Time.time - timeStart) / timeDuration;
            if (u > 1)
            {
                u = 1;
                moving = false;
            }

            Vector3 p01, p12, p23, p012, p123, p0123;
            p01 = (1 - u) * c0.position + u * c1.position;
            p12 = (1 - u) * c1.position + u * c2.position;
            p23 = (1 - u) * c2.position + u * c3.position;

            p012 = (1 - u) * p01 + u * p12;
            p123 = (1 - u) * p12 + u * p23;

            p0123 = (1 - u) * p012 + u * p123;

            transform.position = p0123;
        }
    }

    //players moves with WASD
    void GetPlayerInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * movementSpeed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += (-transform.forward) * movementSpeed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += (-transform.right) * movementSpeed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * movementSpeed * Time.deltaTime;

        }

        switch (playerFaceStatus)
        {
            //top face
            case faceStatus.face1:
                //gravity is down the y axis
                break;
            case faceStatus.face2:
                //gravity is up the z axis
                break;
            case faceStatus.face3:
                // gravity is down the z axis (reverse from face2)
                break;
            case faceStatus.face4:
                //gravity is up the x axis 
                break;
            case faceStatus.face5:
                //gravity is down the x axis (reverse from face4)
                break;
            //bottom face
            case faceStatus.face6:
                //gravity is up the y axis (reverse from face1)
                break;
            default:
                break;
        }
    }

    //movement
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
        transform.localRotation = Quaternion.Euler( transform.localEulerAngles.x , _angle, transform.localEulerAngles.z );


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

    //when player gets to edge
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

    /// <summary>
    /// ATTACK CODE GOES HERE
    /// </summary>
    void OnAttack()
    {
        //runs everytime our char attacks



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Door")
        {
            onDoor = true;
            _rotationTrans = other.gameObject.GetComponent<DoorTrigger>().moveLocation;
            other.gameObject.GetComponent<DoorTrigger>().SwitchDirection();
            //checkToCalculate = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Door")
        {
            onDoor = false;
        }
    }

    //when raycast detects nothing under player, rotates 90 degrees forward
    //instead of v3 use rotation
    //slerp equation
    //NOT IN USE
    void RotatePlayer(ref Vector3 rotatingObj, Vector3 _endPoint, float timeModifier, float timeStart)
    {
        float u = (Time.time - timeStart) / timeModifier;

        if(u >= 1)
        {
            u = 1;
            _rotating = false;
            //_first = true;

            //we have rotates, we can start checking again for missing floor
            _rotationTrans = null;
        }

        Vector3 p1;
        p1 = (1 - u) * rotatingObj + u * _endPoint;
        rotatingObj = p1;
    }
    //when player moves to next side, outside script calls this
    void ChangePlayerFaceStatus(faceStatus newFaceStatus, Transform newPos)
    {
        playerFaceStatus = newFaceStatus;
        //rotation is just as important as position, whatever direction player is facing that is their forward
        transform.position = newPos.position;
        transform.rotation = newPos.rotation;

        //keep track of current rotation with _playerAngle

        
    }
}
