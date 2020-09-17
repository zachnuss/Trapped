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
     *      takeDamage(int attackDamage) //function for enemy to call to do attack damage to player
     */
    /**
     * CLASS VARIABLES
     */
    ///public
    public int health;
    public int damage;
    public float speed;
    public int pointValue;
    [Range(2f, 6f)] public float rateOfBehaviorChange;

    ///protected
    protected Behavior _myBehavior;
    protected CubemapFace myFace { get { return _myFace; } }

    ///private
    private Vector3 _moveDir;
    private CubemapFace _myFace; //getter
    private float _wallDetectRay = 1.0f;
    private bool _hasHitWall = false;

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
        }
    }
    public void levelUp()
    {
        //raise stats
        ///health
        ///damage
        ///speed
        ///pointValue
    }

    ///protected
    protected void _changeBehavior()
    {
        Debug.Log("_changeBehavior: coroutine called");
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
                break;
            case Behavior.ChangeDirection:
                //get random direction to 
                Debug.Log("ChangeDirection hit");
                Direction randDir = _goLeftOrRightDirection();
                _turnThisDirection(randDir);
                break;
            case Behavior.GoForward:
                //get current direction
                outputMoveDir = _moveDir;
                break;
            //case Behavior.AttackPlayer:
                //call attack() function

                //outputMoveDir may need to be slowed down to attack player

                //no break, still track player
            case Behavior.TrackPlayer:
                //outPutMoveDir should move towards the player

                break;
            default: outputMoveDir = Vector3.zero;
                break;
        }

        //edit Vector3 for movement
        //_moveDir = outputMoveDir;
        //randomly change behavior the next time this is called
        _myBehavior = (Behavior)Random.Range(0, 3);//5); //max limit is exclusive
    }

    //
    protected void _attackPlayer()
    {

    }

    //move the player in the direction specified
    protected void _move(Vector3 moveDir)
    {

        //immediately return if zeroed values
        if (moveDir == Vector3.zero) return;
        //change direction if I'm gonna hit the wall
        if (_isEnemyFacingWall())
        {
            Direction outputDir = Direction.Backwards;
            //get rand dir if I've already hit a wall
            if (_hasHitWall) { outputDir = _goLeftOrRightDirection(); }

            _turnThisDirection(outputDir);
            //swap bool
            _hasHitWall = (_hasHitWall) ? false : true;
        }
        //finally move
        transform.position += moveDir * speed * Time.deltaTime;
    }

    //this function will act like onDeath but it doesn't need to be called manually
    protected void OnDestroy()
    {
        //call singleton to add score to game
    }

    ///private
    /*
     *  REGULAR UPDATE() AND AWAKE() RESERVED FOR CHILDREN
     */
    private void Start()
    {
        //initialize variables

        //get where I'm facing
        Vector3 childDir = transform.GetChild(0).position;
        Vector3 initialDir = childDir - transform.position;
        _moveDir = initialDir.normalized;

        /*
         * Determine which face I'm on
         */
        //assume we're on +Y
        _myFace = CubemapFace.PositiveY;
        //Raycast to center (assuming map is placed on (0f, 0f, 0f))
        RaycastHit myFloor;
        if (Physics.Raycast(transform.position, -transform.position, out myFloor, 3f))
        {
            //raycast will shoot directly to floor
            string floorName = myFloor.transform.name;
            //parse name to get face
            /**
             * Stand in code before we have a level with
             * actually named objects in-scene
             */ 
            Debug.LogWarning("BaseEnemy::Start(): Default enemy face set to PositiveY");
            //set default
            _myFace = CubemapFace.PositiveY;
        }

        //loop to change behavior sporatically
        InvokeRepeating("_changeBehavior", 0.5f, rateOfBehaviorChange);
    }
    private void LateUpdate()
    {
        //move enemy
        _move(_moveDir);
    }
    //get random int to cast to Direction enum
    private Direction _goLeftOrRightDirection()
    {
        int randInt = Random.Range(1, 3);//0, 5); //left or right only
        return (Direction)randInt;
    }
    //take a direction, based on 
    private void _turnThisDirection(Direction dir)
    {
        Debug.Log("_turnThisDirection hit: turning " + dir);
        float yAxisTurn = 0f;
        //using myFace for CubemapFace, alter stuff
        switch (dir)
        {
            case Direction.Right:
                yAxisTurn = 90f;
                break;
            case Direction.Left:
                yAxisTurn = -90f;
                break;
            case Direction.Backwards:
                //return positive or negative 180f
                float posOrNeg = (Random.Range(0, 2) == 0) 
                                    ? 180 : -180;
                yAxisTurn = posOrNeg;
                break;
           default: //if forward or null, don't do anything
                break;
        }
        //turn only if there's a value given
        if (yAxisTurn != 0f)
        {
            Vector3 turningVector = new Vector3(0f, yAxisTurn, 0f);
            transform.eulerAngles += turningVector;
            //change facing dir to match rotation
            _moveDir = (transform.GetChild(0).position - transform.position).normalized;
        }
    }

    private bool _isEnemyFacingWall()
    {
        //local vars
        bool isFacingWall = false;
        RaycastHit hit;
        //check what's in front using Raycast
        if (Physics.Raycast(transform.position, _moveDir, out hit, _wallDetectRay))
        {
            //don't change direction if I'm looking at the player
            if (hit.transform.tag != "Player")
                isFacingWall = true;
            //am I hitting myself?
            else if (hit.transform.name == transform.GetChild(0).name)
                Debug.LogWarning("BaseEnemy: hitting child for raycast");
        }

        return isFacingWall;
    }

}
