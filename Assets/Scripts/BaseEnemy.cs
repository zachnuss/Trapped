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

    ///protected
    protected Behavior _myBehavior;

    ///private

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
    }

    ///protected
    //protected IEnumerator changeBehavior(Behavior changingTo)
    //{
    //    switch (changingTo)
    //    {
    //        case Behavior.Idle:
    //            break;
    //        case Behavior.ChangeDirection:
    //            break;
    //        case Behavior.GoForward:
    //            break;
    //        case Behavior.TrackPlayer:
    //            break;
    //        case Behavior.AttackPlayer:
    //            break;
    //    }
    //}
    //this function will act like onDeath but it doesn't need to be called manually
    protected void OnDestroy()
    {
        //call singleton to add score to game
    }

    ///private
    //private Direction getRandomDirection()
    //{
        
    //}


}
