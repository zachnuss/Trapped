//Wesley Morrison
//9/7/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed; //speed modifiable by level designer
    private int damage; //How much damage the projectile does, modifiable by power ups, get from player logic
    private GameObject Controller; //Reference to GameController

    // Start is called before the first frame update
    void Start()
    {
        Controller = GameObject.FindGameObjectWithTag("GameController");
        damage = Controller.GetComponent<PlayerLogic>().damage;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Move_Forward();
    }


    void Move_Forward() //Moves projectile along axis (may later be modified to towards Target)
    {
        transform.Translate(this.transform.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy") //Assuming enemies will have this tag.
        {
            other.GetComponent<BaseEnemy>().health -= damage; //This is normal damage
            if(Random.Range(0f,10f)< 1) //This is a crit
            {
                other.GetComponent<BaseEnemy>().health -= damage;
            }
        }
    }
}
