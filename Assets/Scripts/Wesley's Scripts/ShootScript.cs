//Wesley Morrison
//9/3/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    //Wall turret shoot script //Put in wall turret script
    //Public data so level designers can edit and change things easily
    /*
    public GameObject Enemy_Bullet; //Bullet prefab
    public float shootDelay; //how long after spawning does it wait to shoot
    public float fireRate; //how often does it fire
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", shootDelay, fireRate);
    }

    //Shoot projectile at player
    void Shoot()
    {
        Object.Instantiate(Enemy_Bullet, transform.position, transform.rotation);
    }
    */


    //Player shoot script
    //Put in player input script
    /*
    public GameObject Player_Bullet; //Bullet prefab

    private void Update()
    {
        if (Input.GetButtonDown("joystick button 0")) { //Use input manager to apply a name, then replace joystick button 0 with the new name
            Shoot(); //shoots on button down
        }
    }

    //Shoot projectile at enemy, can update later for a time delay between shots
    void Shoot()
    {
        Object.Instantiate(Player_Bullet, transform.position, transform.rotation);
    }
    */
}
