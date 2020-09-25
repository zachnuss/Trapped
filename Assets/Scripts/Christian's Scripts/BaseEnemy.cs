/*
 * [Author: Christian Mullins]
 * [Summary: Parent class for all enemies in the game.]
 */
using System.Collections;
using UnityEngine;

public enum Behavior
{
    Idle, ChangeDirection, GoForward, TrackPlayer, AttackPlayer
}

public enum Direction
{
    Forward, Right, Left, Backwards, NULL
}

public class BaseEnemy : MonoBehaviour
{
    /*
     *  REQUIRED VALUES AND FUNCTIONS SPECIFIED ON TRELLO:
     *      Health
     *      Damage
     *      Speed
     *      pointValue
     *      levelup()
     *      onDeath() //gives points to player
     *      takeDamage(int attackDamage) //function for enemy to call to do 
     *                                      attack damage to player
     */
    /**
     * CLASS VARIABLES
     */
    ///public
    public int health;
    public int damage;
    public float speed;
    public int pointValue;
    [Range(1f, 5f)] public float rateOfBehaviorChange = 2f;

    ///protected
    protected int _maxHealth;
    protected Behavior _myBehavior;
    protected CubemapFace myFace { get { return _myFace; } }
    protected bool _isTrackingPlayer = false;

    ///private
    private Vector3 _moveDir; //movement
    private Vector3 _rotVal; //rotation
    private CubemapFace _myFace; //getter
    private float _wallDetectRay = 1.0f;
    private bool _hasHitWall = false;
    private GameObject _playerGO; //initialize in start

    /**
     * CLASS FUNCTIONS
     */
    ///public
    public void takeDamage(int attackDamage)
    {
        //take health away
        health -= attackDamage;
        //did the enemy die?
        if (health < 1)
        {
            health = 0;
            //destroy
            Destroy(gameObject);
            //set game over screen
        }
    }
    public void levelUp()
    {
        //raise stats
        health = Mathf.FloorToInt(health * 1.1f);
        damage = Mathf.FloorToInt(damage * 1.1f);
        speed = Mathf.FloorToInt(speed * 1.1f);
        pointValue = Mathf.FloorToInt(pointValue * 1.1f);
    }

    //takes a float value and puts it into a vector3 for appropriate
    //turning based on which CubeFace the enemy is on
    protected Vector3 _getTurningAxis(float axisVal)
    {
        //zero out your output vector3
        Vector3 outputVector = Vector3.zero;
            
        switch (myFace)
        {
            case CubemapFace.PositiveX:
                outputVector = new Vector3(axisVal, 0f, 0f);
                break;
            case CubemapFace.NegativeX:
                outputVector = new Vector3(-axisVal, 0f, 0f);
                break;
            case CubemapFace.PositiveY:
                outputVector = new Vector3(0f, axisVal, 0f);
                break;
            case CubemapFace.NegativeY:
                outputVector = new Vector3(0f, -axisVal, 0f);
                break;
            case CubemapFace.PositiveZ:
                outputVector = new Vector3(axisVal, 0f, 0f);
                break;
            case CubemapFace.NegativeZ:
                outputVector = new Vector3(0f, 0f, -axisVal);
                break;
            //Unknown and default returns zeroed Vector3
            case CubemapFace.Unknown:
            default:
                break;
        }
        return outputVector;
    }

    ///protected
    protected void _changeBehavior()
    {
        //transfer rate of behavior change and get wait time
        float randWaitTime = Random.Range(0f, rateOfBehaviorChange);
        Vector3 outputMoveDir = Vector3.zero;

        switch (_myBehavior)
        {
            case Behavior.Idle:
                //zero out Vector3 to prevent any movement
                outputMoveDir = Vector3.zero;
                //increase randWaitTime
                randWaitTime += 3.0f;
                _isTrackingPlayer = false;
                break;
            case Behavior.ChangeDirection:
                //get random direction to 
                Debug.Log("ChangeDirection hit");
                Direction randDir = _goLeftOrRightDirection();
                _turnThisDirection(randDir);
                _isTrackingPlayer = false;
                break;
            case Behavior.GoForward:
                //get current direction
                outputMoveDir = _moveDir;
                _isTrackingPlayer = false;
                break;
            case Behavior.AttackPlayer:
                //call attack() function

                //outputMoveDir may need to be slowed down to attack player

                //no break, still track player
            case Behavior.TrackPlayer:
                //this bool will override instructions for _move()
                _isTrackingPlayer = true;
                //outPutMoveDir should move towards the player

                break;
            default: outputMoveDir = Vector3.zero;
                _isTrackingPlayer = false;
                break;
        }
        //randomly change behavior the next time this is called
        _myBehavior = (Behavior)Random.Range(0, 3); //max limit is exclusive
        //CHANGE RANGE WHEN RELEASING BUILD
    }

    //Reserved function for enemies that will attack the player other than by
    //simply tracking the player
    protected virtual void _attackPlayer()
    {
        //left blank for inheritted children
    }

    //default attack and tracker for inheritters of BaseEnemy
    protected Transform _trackPlayer()
    {
        //get players location and location to move
        Vector3 playerPos = _playerGO.transform.position;
        Vector3 dirToMove = Vector3.MoveTowards(transform.position,
                                                playerPos,
                                                Time.maximumDeltaTime);
        dirToMove = Vector3.Normalize(dirToMove); // normalize for movement
        //check if there's a wall between us
        if (_isEnemyFacingWall())
        {
            //create local vars
            Vector3 longestAxis = Vector3.zero;
            Vector3 side1 = Vector3.zero;
            Vector3 side2 = Vector3.zero;
            //find longest axis
            for (int i = 0; i < 3; i++)
            {
                //assign axis to appropriate variables
                if (Mathf.Abs(dirToMove[i]) == 1f) { longestAxis[i] = 1f; }
                else if (side1 == Vector3.zero) { side1[i] = dirToMove[i]; }
                else if (side2 == Vector3.zero) { side2[i] = 1f; }
            }

            //check which direction will bring me closest to the player
            side1 += transform.position;
            side2 += transform.position;
            if (Vector3.Distance(side1, playerPos) < Vector3.Distance(side2, playerPos))
            {
                //side 1 is closer
                dirToMove = side1;
            }
            else if (Vector3.Distance(side1, playerPos) > Vector3.Distance(side2, playerPos))
            {
                //side 2 is closer
                dirToMove = side2;
            }
                //if other, swap values
        }
        ///         apply movement
        ///         apply relative rotation???
        //get rotational stuff and pack into Transform for return
        Transform output = transform;
        //output.position = dirToMove;
        output.LookAt(_playerGO.transform, Vector3.up);
        output.position = dirToMove;
        return output;
    }

    //move the player in the direction specified
    protected void _move(Vector3 moveDir, Vector3 rotVal)
    {
        //immediately return if zeroed values
        if (moveDir == Vector3.zero) return;
        //change direction if I'm gonna hit the wall
        if (_isEnemyFacingWall())
        {
            Direction outputDir = Direction.Backwards;
            //get rand dir if I've already hit a wall
            if (_hasHitWall) { outputDir = _goLeftOrRightDirection();
                //Debug.Log("feeeeeerrrrrrppp");
            }

            _turnThisDirection(outputDir);
            //swap bool
            _hasHitWall = (_hasHitWall) ? false : true;
        }
        //finally move
        //transform.position += moveDir * speed * Time.deltaTime;
        if (_isTrackingPlayer)
        {
            //store and unpack Transform
            Transform outputTrans = _trackPlayer();
//            transform.localRotation.eulerAngles.y += outputTrans.localEulerAngles.y * Time.deltaTime;
            //transform.position += outputTrans.position * speed * Time.deltaTime;
        }
        //else
        //{
            transform.position += moveDir * speed * Time.deltaTime;
        //}
            //apply rotational value
            //transform.rotation.eulerAngles += rotVal;
    }

    protected virtual void Update()
    {
        //for bug testing
        //if (Input.GetKeyDown(KeyCode.Space))
          //  _myBehavior = Behavior.TrackPlayer;
        /**
         *  TO DO
         *  1. Check if I'm trackig/attacking the player
         *          -get new direction and apply to "_moveDir"
         *          -get my new rotation and apply
         * 
         */ 
        //if (_myBehavior == Behavior.TrackPlayer || _myBehavior == Behavior.AttackPlayer)
        //{
            
        //}
        //move enemy
        _move(_moveDir, Vector3.zero);
    }
    //this function will act like onDeath (doesn't need to be called manually)
    protected virtual void OnDestroy()
    {
        //call singleton to add score to game

        ///collectable
        ///STAND IN CODE FOR THE FUTURE
        float dropChance = 0.3f;
        GameObject droppedCol = null; //= getCollectable();
        if (Random.Range(0.0f, 1.0f) <= dropChance)
        {
            //make sure this doesn't parent to the enemy on Instantiation
            //(otherwise it will be destroyed immediately)
            //Instantiate(droppedCol, transform, true);
        }
    }

    ///private
    /*
     *  AWAKE() RESERVED FOR CHILDREN
     */
    private void Start()
    {
        //initialize variables

        //get where I'm facing
        Vector3 childDir = transform.GetChild(0).position;
        Vector3 initialDir = childDir - transform.position;
        _moveDir = initialDir.normalized;

        ///for debugging shit
        _playerGO = GameObject.Find("NewPlayer");

        //Raycast to center (assuming map is placed on (0f, 0f, 0f))
        RaycastHit myFloor;
        if (Physics.Raycast(transform.position, - transform.position, 
                out myFloor, 3f))
        {
            //raycast will shoot directly to floor
            string floorName = myFloor.transform.name;
            //parse name to get face
            /**
             * Stand in code before we have a level with
             * actually named objects in-scene
             */ 
            Debug.LogWarning("BaseEnemy::Start(): Default enemy face " +
                                "set to PositiveY");
            //set default
            //_myFace = CubemapFace.PositiveX;

        }
        //loop to change behavior sporatically
        InvokeRepeating("_changeBehavior", 0.5f, rateOfBehaviorChange);
    }

    //get random int to cast to Direction enum
    private Direction _goLeftOrRightDirection()
    {
        int randInt = Random.Range(1, 3);//left or right only
        return (Direction)randInt;
    }

    //take a direction, based on 
    private void _turnThisDirection(Direction dir)
    {
        Debug.Log("_turnThisDirection hit: turning " + dir);

        //the type of axis turn will be determined by CubemapFace
        float axisTurn = 0f;
        //using myFace for CubemapFace, alter stuff
        switch (dir)
        {
            case Direction.Right:
                axisTurn = 90f;
                break;
            case Direction.Left:
                axisTurn = -90f;
                break;
            case Direction.Backwards:
                //return positive or negative 180f
                float posOrNeg = (Random.Range(0, 2) == 0) ? 180 : -180;
                axisTurn = posOrNeg;
                break;
           default: //if forward or null, don't do anything
                break;
        }
        //turn only if there's a value change
        if (axisTurn != 0f)
        {
            //get appropriate turning axis based on CubemapFace
            Vector3 turningVector = Vector3.zero;// = _getTurningAxis(axisTurn);
            //PositiveY  is
            turningVector.y = axisTurn;
            //transform.Rotate(turningVector); //mess with local vs world space
            transform.localEulerAngles += turningVector;
            //change facing dir to match rotation
            _moveDir = (transform.GetChild(0).position 
                        - transform.position).normalized;
        }
    }

    private bool _isEnemyFacingWall()
    {
        //local vars
        bool isFacingWall = false;
        RaycastHit hit;
        //check Behavior
        if (_myBehavior != Behavior.AttackPlayer && _myBehavior != Behavior.TrackPlayer)
        {
            //draw line for debugging
            Vector3 endPoint = transform.position + _moveDir;
            //Debug.DrawLine(transform.position, endPoint, Color.green, Time.deltaTime, false);
            Debug.DrawRay(transform.position, _moveDir, Color.green, Time.deltaTime, false);
            //check what's in fron using Raycast
            if (Physics.Raycast(transform.position, _moveDir, out hit, _wallDetectRay))
            {
                //don't change direction if I'm looking at the player
                if (hit.transform.tag != "Player")
                { isFacingWall = true; }
                //am I hitting myself?
                else if (hit.transform.name == transform.GetChild(0).name)
                { Debug.LogWarning("BaseEnemy: hitting child for raycast"); }
            }
        }
        //else
        //{
            //attack and track behavior

            
        //}

        return isFacingWall;
    }

    //takes the direction the enemy is moving at the player
    //and calculate the rotation to keep the enemy facing the player
    private Vector3 _getTrackingRotation()
    {
        float rotationVal = 0f;
        float enemyToPlayerDist = Vector3.Distance(transform.position,
                                                   _playerGO.transform.position);
        //float h = 
        //get rotational difference between rotation to look at
        /*
         * ENTER MATH STUFF:
         *      CALULATE ISCOSCELES TRIANGLE sides
         * 
         * 
         */
        ///values to use
        ///_rotValue
        //get my move direction

        //translate and turn
        ///Vector3 _getTurningAxis(float axisVal)
        return new Vector3(0f, rotationVal, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if the enemy is going to collide with a door, turn around
        //this should solve potential bugs in the future
        if (other.transform.tag == "Door")
        {
            //turn around if the enemy's about to hit the door
            _turnThisDirection(Direction.Backwards);
        }
    }
}
