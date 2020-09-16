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
    [Range(5f, 10f)] public float rateOfBehaviorChange;

    ///protected
    protected Behavior _myBehavior;
    protected CubemapFace myFace { get { return _myFace; } }

    ///private
    private Vector3 _moveDir; //last direction moved
    private CubemapFace _myFace; //getter

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
    protected IEnumerator _changeBehavior(Behavior changingTo)
    {
        //transfer rate of behavior change and get wait time
        float randWaitTime = Random.Range(1f, rateOfBehaviorChange);
        Vector3 outputMoveDir;

        switch (changingTo)
        {
            case Behavior.Idle:
                //zero out Vector3 to prevent any movement
                outputMoveDir = Vector3.zero;
                //increase randWaitTime
                randWaitTime += 3.0f;
                break;
            case Behavior.ChangeDirection:
                //get random direction to 
                break;
            case Behavior.GoForward:
                //get current direction
                outputMoveDir = _moveDir;
                break;
            case Behavior.AttackPlayer:
                //call attack() function

                //outputMoveDir may need to be slowed down to attack player

                //no break, still track player
            case Behavior.TrackPlayer:
                //outPutMoveDir should move towards the player

                break;
        }

        //edit Vector3 for movement
        _moveDir = outputMoveDir;
        //randomly change behavior the next time this is called
        _myBehavior = (Behavior)Random.Range(0, 5); //max limit is exclusive

        yield return new WaitForSeconds(randWaitTime);
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

        //move enemy
        if (_isEnemyFacingWall() == true)
        transform.position += moveDir * speed * Time.deltaTime;
    }

    //this function will act like onDeath but it doesn't need to be called manually
    protected void OnDestroy()
    {
        //call singleton to add score to game
    }

    ///private
    /*
     *  REGULAR UPDATE() RESERVED FOR CHILDREN AND START()
     */ 
    private void LateUpdate()
    {
        //move enemy
        _move(_moveDir);
    }
    private void Start()
    {
        //initialize variables

        /*
         * Determine which face I'm on
         */
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

        //call coroutine to change behavior
        StartCoroutine(_changeBehavior(_myBehavior));
    }
    //get random int to cast to Direction enum
    private Direction getRandomDirection()
    {
        int randInt = Random.Range(0, 5);
        return (Direction)randInt;
    }
    //take a direction, based on 
    private Vector3 _translateDirectionToSpace(Direction dir)
    {
        Vector3 translated;

        //using myFace for CubemapFace, alter stuff
        switch (dir)
        {
            //take initial walking direction
            case Direction.Forward: translated = _moveDir;
                break;

            case Direction.Right:
                //get dominate move direction
                Vector3 dir
                //iterate to what the relative "right" would be

                break;

            case Direction.Left:
                //get right movement and make negative
                break;

            //take initial walking direction and subtract it
            case Direction.Backwards: translated = -_moveDir;
                break;

            case Direction.NULL:
            default: translated = Vector3.zero;
                break;
        }

        return translated;
    }

    private bool _isEnemyFacingWall(Direction dir)
    {
        //local vars
        bool isFacingWall = false;
        Vector3 dirToWorldSpace = _translateDirectionToSpace(dir);
        RaycastHit hit;
        //check what's in front using Raycast
        if (Physics.Raycast(transform.position, dirToWorldSpace, out hit, 1.5f))
        {
            //don't change direction if I'm looking at the player
            if (hit.transform.tag != "Player")
                isFacingWall = true;
        }

        return isFacingWall;
    }

    //takes in Vector3 and returns a Vector3 that's zeroed out
    //on every axis except the longest
    private Vector3 _returnLongestAxis(Vector3 vec)
    {
        //local values
        float longestAxis = 0;
        int longestIndex = 0;
        //loop to find longest axis
        for (int i = 0; i < 3; i++)
        {
            if (Mathf.Abs(vec[i]) > longestAxis)
            {
                longestAxis = Mathf.Abs(vec[i]);
                longestIndex = i;
            }
        }
        //create output vector
        Vector3 outputVector = Vector3.zero;
        outputVector[longestIndex] = longestAxis;

        return outputVector;
    }
}
