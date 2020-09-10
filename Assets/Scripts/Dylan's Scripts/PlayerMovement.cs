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
/// - Player rotates on local axis so it depends on which way player is rotated
/// - Will incorate a shoot function
/// - Movement is based on joystick orientation
/// - shooting i buttom button
/// </summary>


public class PlayerMovement : MonoBehaviour
{
    //set up for rotation and new rotation orientation
    [Header("Parent object of this player obj")]
    public GameObject parent;
    //new rotation orientation player moves to
    Quaternion targetRotation;
    //PlayerInputActions controls;
    Vector3 _playerAngle;
    [Header("Player movement speed")]
    public float movementSpeed = 1.0f;
    Vector2 movementInput;
    //rotation
    float _turnSpeed = 20f;
    float _angle;
    

    //Camera
    public Camera playerCam;
    //level setup script

    //when we have successfully rotated
    bool _rotating = false;
    bool _first = true;
    [Header("Shows if player is off the edge")]
    public bool overTheEdge = false;
    bool onDoor = false;

    //when we hit a door the player rotates and moves to this transform taken from the door prefab
    private Transform _rotationTrans;

    /// <summary>
    /// Interpolation with sin easing, most of these will be private
    /// may use c2 and c3 for bezier or multiple interpolation points in future
    /// </summary>
    private Transform c0, c1;
    private float timeDuration = 1f;
    private bool checkToCalculate = false;
    private Vector3 p01;
    //public Color c01;
    private Quaternion r01;
    //public Vector3 s01;
    private bool moving = false;
    private float timeStart;
    private float u;

    //Shoot Code Variable
    public GameObject Player_Bullet; //Bullet prefab

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
        //movement
        //detects if player is over an edge
        if (DetectEdge())
        {
            overTheEdge = true;
        }
        //moves player to next side of cube
        if(_rotationTrans != null)
        {
            Interpolation();
        }
        //Interpolation stuff, for rotation onto next side
        if (overTheEdge && onDoor && !checkToCalculate && !moving)
        {
            //Debug.Log("got trans, start interpolation");
            //if we hit the door and are off the cube
            checkToCalculate = true;
        }


    }

    //moves player based on equation
    //may need to update to bezier for smoother rotation
    void Interpolation()
    {
        if (checkToCalculate)
        {
            OnPlayerRotation();
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

    void OnPlayerRotation()
    {
        //runs when player moves to next cube (runs only once)
        //camera rotation
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
    /// ATTACK CODE GOES HERE, WHESLEY'S SHOOTING GOES HERE
    /// </summary>
    void OnAttack()
    {
        //runs everytime our char attacks
        //Wesley-Code
        Object.Instantiate(Player_Bullet, transform.position, transform.rotation);


    }

    /// <summary>
    /// Attack code goes here
    /// 
    /// </summary>
    /// <param name="other"></param>
    


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










    //NOT IN USE
    //when raycast detects nothing under player, rotates 90 degrees forward
    //instead of v3 use rotation
    //slerp equation
    //when player moves to next side, outside script calls this
    //void ChangePlayerFaceStatus(faceStatus newFaceStatus, Transform newPos)
    //{
    //playerFaceStatus = newFaceStatus;
    //rotation is just as important as position, whatever direction player is facing that is their forward
    //transform.position = newPos.position;
    //transform.rotation = newPos.rotation;
    //keep track of current rotation with _playerAngle
    //}

    //players moves with WASD
    //NOT IN USE
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


    }
    //NOT IN USE
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

            //Vector3 p01, p12, p23, p012, p123, p0123;
            //p01 = (1 - u) * c0.position + u * c1.position;
            //p12 = (1 - u) * c1.position + u * c2.position;
            //p23 = (1 - u) * c2.position + u * c3.position;

            //p012 = (1 - u) * p01 + u * p12;
           // p123 = (1 - u) * p12 + u * p23;

            //p0123 = (1 - u) * p012 + u * p123;

            //transform.position = p0123;
        }
    }
}
