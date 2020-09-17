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
    public GameObject follower;
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

    [Header("Current Player Stats - Set on Scene Start")]
    public int health;
    public int damage;
    public int speedMultiplier;

    //Camera
   // public CamLookAt playerCam;
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
    private Transform c0, c1, c2;
    private Quaternion r01;
    private float timeDuration = 1.2f;
    float timeDurationCamera = 1.5f;
    private bool checkToCalculate = false;
    private Vector3 p01;

    //for smooth parent rotation
    private Quaternion par01;
    private Transform pc1, pc0;

    public EasingType easingTypeC = EasingType.linear;
    private bool moving = false;
    private float timeStart;
    private float u, u2;
    float easingMod = 2f;

    //Shoot Code Variable
    [Header("Player Bullet Var")]
    public GameObject Player_Bullet; //Bullet prefab

    //player data scriptable obj
    [Header("Player Data")]
    public PlayerData playerData;

    //awake
    private void Awake()
    {
  
        Vector3 _playerAngle = Vector3.zero;
 
    }

    // Start is called before the first frame update
    void Start()
    {

        SetPlayerStats();
    }

    // Used for physics 
    void FixedUpdate()
    {
        //cant move if we are rotating
        if(!overTheEdge && !moving)
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
            //Bezier();
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
            
            //Debug.Log("Moving to next side YEET");
            c0 = this.transform;
            c1 = _rotationTrans;

            //smooth parent movement (for camera)
            pc0 = parent.transform;
            pc1 = _rotationTrans;
            
            checkToCalculate = false;
            moving = true;
            timeStart = Time.time;
            //timeStart2 = Time.time;
            OnPlayerRotation();

           
        }

        if (moving)
        {
            //Debug.Log("moving");
            u = (Time.time - timeStart) / timeDuration;
           // u2 = (Time.time - timeStart2) / timeDurationCamera;

            if (u >= 1)
            {
                //when we reach new pos
                parent.transform.rotation = _rotationTrans.transform.rotation;
              
                u = 1;
                moving = false;
                _rotationTrans = null;
                overTheEdge = false;
                //Debug.Log("IT REACHED THE END HOLY CRAP IT WORKED IMMA SLEEP NOW GGS");
            }

            //adjsut u value to the ranger from uMin to uMax
            //different types of eases to avoid snaps and rigidness
            u2 = EaseU(u2, easingTypeC, easingMod);
            //interpolation equation (with min and max)
            //u = ((1 - u) * uMin) + (u * uMax);

            //standard linear inter
            //position
            p01 = (1 - u) * c0.position + u * c1.position;

            //quaternions are different
            //use unities built in spherical linear interpolation
            //SLERP
            r01 = Quaternion.Slerp(c0.rotation, c1.rotation, u);

            par01 = Quaternion.Slerp(pc0.rotation, pc1.rotation, u2);

           // Vector3 tempRot = new Vector3(0, -90, 0);
            //apply those new values
            transform.position = p01;
            transform.rotation = r01;
            follower.transform.rotation = r01;
            parent.transform.rotation = par01;
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

        //base movement is just 1.0
        movementSpeed = movementSpeed + (movementSpeed * speedMultiplier);

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
        Transform newCameraTrans = _rotationTrans;
        
        

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

    void OnAttack()
    {
        //runs everytime our char attacks
        //Wesley-Code
        GameObject bullet = Object.Instantiate(Player_Bullet, transform.position, transform.rotation);
        //ZACHARY ADDED THIS
        StartCoroutine(bullet.GetComponent<ProjectileScript>().destroyProjectile());
        //just to destroy stray bullets if they escape the walls
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(bullet.transform.forward * 1000);



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
            c2 = other.gameObject.GetComponent<DoorTrigger>().moveMid;
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


    //easing types
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

    //setting player stats
    void SetPlayerStats()
    {
        
        health = playerData.totalHealthBase + playerData.healthUpgrade;

        damage = playerData.totalDamageBase + playerData.damageUpgrade;

        speedMultiplier = (playerData.speedUpgrade)/10;

        
    }



}
