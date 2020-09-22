/*
 * [Author: Christian Mullins]
 * [Summary: This script controls the bullets movement.]
 */
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    ///private
    private Vector3 _startPos;
    private Vector3 _fireDirection;
    private float _speed;
    private float _maxBulletDistance;


    /*
     * Start must be used instead of Awake to properly initialize
     * variables and maintain scaling on instantiation.
     */ 
    void Start()
    {
        //initialize values
        _startPos = transform.position;
        //get values from parent
        _speed = GetComponentInParent<WallTurret>().bulletSpeed;
        _fireDirection = GetComponentInParent<WallTurret>().fireDirection;
        _maxBulletDistance = GetComponentInParent<WallTurret>().maxBulletDistance;
    }

    void Update()
    {
        transform.position += _fireDirection * _speed * Time.deltaTime;
        //check distance traveled
        if (Vector3.Distance(_startPos, transform.position) > _maxBulletDistance)
            Destroy(gameObject);
    }

    //destroy whenever it touches another object
    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
