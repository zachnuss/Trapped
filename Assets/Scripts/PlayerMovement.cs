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
    //PlayerInputActions controls;
    Vector3 _playerAngle;

    //tells current face player is resting on
    public faceStatus playerFaceStatus;
    //current player position
    //public Vector3 playerPos;
    //current player velocity
    //public Vector3 playerVelocity;

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

    //movement

    private void OnMove(InputValue value)
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
        //Debug.Log(_angle);

        switch (playerFaceStatus)
        {
            case faceStatus.none:
                break;
            case faceStatus.face1:
                //rotates based on what side we are on
                //0, angle, 0
                targetRotation = Quaternion.Euler(0, _angle, 0);
                break;
            case faceStatus.face2:
                //90, 0, angle
                //angle - 90, - 90, 90
                //cam = 0, 180, 0
                targetRotation = Quaternion.Euler(_angle - 90, -90, 90);
                break;
            case faceStatus.face3:
                //-90, 0, angle
                //angle - 90, 270, 90
                //cam = 0, 0, 0
                targetRotation = Quaternion.Euler(_angle - 90, -270, 90);
                break;
            case faceStatus.face4:
                //angle, 0, -90
                //angle - 90, 0, -90
                //cam = 0, -90, 0
                targetRotation = Quaternion.Euler(_angle - 90, 0, -90);
                break;
            case faceStatus.face5:
                //angle, 0, 90
                //-angle - 90, 0, 90
                //cam = 0, 90, 0
                targetRotation = Quaternion.Euler(-_angle - 90, 0, 90);
                break;
            case faceStatus.face6:
                //0, angle, -180
                //cam = -90, 0, 0
                targetRotation = Quaternion.Euler(0, -_angle - 180, -180);
                break;
            default:
                break;
        }
        

        //Debug.Log(targetRotation);
        transform.rotation = targetRotation;
        //player is always moving forward, player is just adjsuting which way they move forward (always local forward so we can have player move consistentaly forward on each side)
        //this.GetComponent<Rigidbody>().MovePosition(transform.position + (transform.forward * movementSpeed * Time.deltaTime));
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }

    void Movement()
    {
        Vector3 movement = MainJoystick();
        //Debug.Log(movement);
        //only move if player gives input
        if(movement != Vector3.zero)
            RotateMovement(movement);
    }


    //when player moves to next side, outside script calls this
    void ChangePlayerFaceStatus(faceStatus newFaceStatus, Transform newPos)
    {
        playerFaceStatus = newFaceStatus;
        //rotation is just as important as position, whatever direction player is facing that is their forward
        transform.position = newPos.position;
        transform.rotation = newPos.rotation;

        //keep track of current rotation with _playerAngle

        //sets new player angle based on newStatus
        switch (playerFaceStatus)
        {
            //each face has 4 possible directions
            case faceStatus.none:
                break;
            case faceStatus.face1:
                //S2, S3, S4, S5
                switch (newFaceStatus)
                {
                    case faceStatus.face2:
                        _playerAngle.x += 90;
                        break;
                    case faceStatus.face3:
                        _playerAngle.x -= 90;
                        break;
                    case faceStatus.face4:
                        _playerAngle.z -= 90;
                        break;
                    case faceStatus.face5:
                        _playerAngle.z += 90;
                        break;
                    default:
                        break;
                }
                break;
            case faceStatus.face2:
                //S1, S4, S5, S6
                switch (newFaceStatus)
                {
                    case faceStatus.face1:
                        _playerAngle.x -= 90;
                        break;
                    case faceStatus.face4:
                        _playerAngle.z -= 90;
                        break;
                    case faceStatus.face5:
                        _playerAngle.z += 90;
                        break;
                    case faceStatus.face6:
                        _playerAngle.x += 90;
                        break;
                    default:
                        break;
                }
                break;
            case faceStatus.face3:
                //S1, S4, S5, S6
                switch (newFaceStatus)
                {
                    case faceStatus.face1:
                        _playerAngle.x += 90;
                        break;
                    case faceStatus.face4:
                        _playerAngle.z -= 90;
                        break;
                    case faceStatus.face5:
                        _playerAngle.z += 90;
                        break;
                    case faceStatus.face6:
                        _playerAngle.x += 90;
                        break;
                    default:
                        break;
                }
                break;
            case faceStatus.face4:
                //S1, S2, S3, S6
                switch (newFaceStatus)
                {
                    case faceStatus.face1:
                        _playerAngle.x += 90;
                        break;
                    case faceStatus.face2:
                        _playerAngle.y -= 90;
                        break;
                    case faceStatus.face3:
                        _playerAngle.y += 90;
                        break;
                    case faceStatus.face6:
                        _playerAngle.x += 90;
                        break;
                    default:
                        break;
                }
                break;
            case faceStatus.face5:
                //S1, S2, S3, S6
                switch (newFaceStatus)
                {
                    case faceStatus.face1:
                        _playerAngle.x -= 90;
                        break;
                    case faceStatus.face2:
                        _playerAngle.y += 90;
                        break;
                    case faceStatus.face3:
                        _playerAngle.y -= 90;
                        break;
                    case faceStatus.face6:
                        _playerAngle.x += 90;
                        break;
                    default:
                        break;
                }
                break;
            case faceStatus.face6:
                //S2, S3, S4, S5
                switch (newFaceStatus)
                {
                    case faceStatus.face4:
                        _playerAngle.z += 90;
                        break;
                    case faceStatus.face2:
                        _playerAngle.x -= 90;
                        break;
                    case faceStatus.face3:
                        _playerAngle.x += 90;
                        break;
                    case faceStatus.face5:
                        _playerAngle.z -= 90;
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }
}
