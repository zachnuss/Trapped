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
    PlayerInputActions controls;


    //tells current face player is resting on
    public faceStatus playerFaceStatus;
    //current player position
    public Vector3 playerPos;
    //current player velocity
    public Vector3 playerVelocity;

    public float movementSpeed = 1.0f;
    Vector2 movementInput;
    //rotation
    float _turnSpeed = 20f;
    float _angle;
    Quaternion targetRotation;

    //Camera
    public Camera playerCam;

    //awake
    private void Awake()
    {
        controls = new PlayerInputActions();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerFaceStatus = faceStatus.face1;
    }

    // Used for physics 
    void FixedUpdate()
    {
        Movement();
    }

    // used for updating values and variables
    private void Update()
    {
        //GetPlayerInput();
    }

    //players moves with WASD
    void GetPlayerInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * movementSpeed * Time.deltaTime;

        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.position += (-transform.forward) * movementSpeed * Time.deltaTime;

        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.position += (-transform.right) * movementSpeed * Time.deltaTime;

        }
        if(Input.GetKey(KeyCode.D))
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

    /// <summary>
    /// NOTE: NEEDS PACKAGE
    /// </summary>
    /// <returns></returns>



    //movement

    private void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    //joysticks
    public float JoystickH()
    {
        float r = 0.0f;
        float h = movementInput.x;
        r += Mathf.Round((h * 100f) / 100f);
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    public float JoystickV()
    {

        float r = 0.0f;
        float v = movementInput.y;
        r += Mathf.Round((v * 100f) / 100f);
        //r -= 1;
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public Vector3 MainJoystick()
    {
        return new Vector3(JoystickH(), 0, JoystickV());
    }

    void RotateMovement(Vector3 movement)
    {
        _angle = Mathf.Atan2(movement.x, movement.z);
        _angle = Mathf.Rad2Deg * _angle;

        targetRotation = Quaternion.Euler(0, _angle, 0);
        transform.rotation = targetRotation;

        this.GetComponent<Rigidbody>().MovePosition(transform.position + (transform.forward * movementSpeed * Time.deltaTime));
    }

    void Movement()
    {
        Vector3 movement = MainJoystick();
        RotateMovement(movement);
    }


    //when player moves to next side, outside script calls this
    void ChangePlayerFaceStatus(faceStatus newFaceStatus, Transform newPos)
    {
        playerFaceStatus = newFaceStatus;
        //rotation is just as important as position, whatever direction player is facing that is their forward
        transform.position = newPos.position;
        transform.rotation = newPos.rotation;
    }
}
