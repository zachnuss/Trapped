//Wesley Morrison
//9/7/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed; //speed modifiable by level designer
    public int damage; //How much damage the projectile does, modifiable by power ups

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move_Forward();
    }


    void Move_Forward() //Moves projectile along axis (may later be modified to towards Target)
    {
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }
}
