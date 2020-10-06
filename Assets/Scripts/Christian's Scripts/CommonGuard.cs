using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonGuard : BaseEnemy
{
    private float storeRegSpeed;


    void Awake()
    {
        storeRegSpeed = speed;
    }

    void LateUpdate()
    {
        //check if the player is in front of me or to the left or right
        Direction dirOfPlayer = _isPlayerInRange();
        if (dirOfPlayer != Direction.NULL)
        {
            _isTrackingPlayer = true;
            //move in the desired direction
            _turnThisDirection(dirOfPlayer);
            //increase speed for tracking
            speed = storeRegSpeed * 1.3f;
        }
        else
        {
            _isTrackingPlayer = false;
            //reverse tracking speed to normal
            speed = storeRegSpeed;
        }
    }

}