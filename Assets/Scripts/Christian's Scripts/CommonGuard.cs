using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonGuard : BaseEnemy
{
    //new public values
    [Header("How far away can I see the player?")]
    public float playerRangeCheck = 10.0f;

    ///protected
    protected bool canSprint = true;
    ///private
    private float storeRegSpeed;

    private GameObject _leftDirGO, _rightDirGO;

    void Awake()
    {
        storeRegSpeed = speed;
        //get directional children
        for (int i = 0; i < 3; ++i)
        {
            GameObject assigningGO = transform.GetChild(i).gameObject;
            //assign GO
            if (assigningGO.name == "LeftChild")
                _leftDirGO = assigningGO;
            else if (assigningGO.name == "RightChild")
                _rightDirGO = assigningGO;
        }
    }

    protected override void Update()
    {
        //Debug.Log("curSpeed: " + speed);
        //check if the player is in front of me or to the left or right
        Direction dirOfPlayer = _isPlayerInRange();
        if (dirOfPlayer != Direction.NULL)
        {
            _isTrackingPlayer = true;
            //move in the desired direction
            _turnThisDirection(dirOfPlayer);


            ///suspend changing behavior
            CancelInvoke("_changeBehavior");

            //sprint
            if (canSprint)
            {
                //increase speed for tracking
                speed = storeRegSpeed * 1.5f;
                Invoke("sprintCoolDown", 3f); //returns speed back when invoked
                canSprint = false;
            }
        }
        else
        {
            ///resume change behavior
            if (_isTrackingPlayer)
            {
                InvokeRepeating("_changeBehavior", 0f, 3f);
            }

            _isTrackingPlayer = false;
        }

        //move enemy
        _move(_moveDir);
    }

    //if the player is never hit, then return Direction.NULL
    protected override Direction _isPlayerInRange()
    {
        Direction dirOfPlayer = Direction.NULL;
        float castDist = playerRangeCheck;
        RaycastHit hit;

        //get location of child GOs

        Vector3 lookForward = transform.GetChild(0).position - transform.position;
        Vector3 lookRight = _rightDirGO.transform.position - transform.position;
        Vector3 lookLeft = _leftDirGO.transform.position - transform.position;


        //look forward
        if ((  Physics.Raycast(transform.position, lookForward + lookRight, out hit, castDist / 2f)
            || Physics.Raycast(transform.position, lookForward + lookRight, out hit, castDist / 2f)
            || Physics.Raycast(transform.position, lookForward, out hit, castDist))
            && hit.transform.tag == "Player")
        {
            dirOfPlayer = Direction.Forward;
        }
        //look left
        else if (Physics.Raycast(transform.position, lookLeft, out hit, castDist)
            && hit.transform.tag == "Player")
        {
            dirOfPlayer = Direction.Left;
        }
        //look right
        else if (Physics.Raycast(transform.position, lookRight, out hit, castDist)
            && hit.transform.tag == "Player")
        {
            dirOfPlayer = Direction.Right;
        }

        ///draw raycast in space to debug
        //draw forward
        //Debug.DrawRay(transform.position, lookForward.normalized, Color.black, 0.2f, false);
        //draw left
        //Debug.DrawRay(transform.position, lookLeft.normalized, Color.red, 0.2f, false);
        //draw right
        //Debug.DrawRay(transform.position, lookRight.normalized, Color.blue, 0.2f, false);

        return dirOfPlayer;
    }


    //call as invoke so the 
    protected void sprintCoolDown()
    {
        speed = storeRegSpeed;
        canSprint = true;
    }
}