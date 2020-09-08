/*
 * [Author: Christian Mullins]
 * [Summary: This script controls the behavior of the 
 *      WallTurret enemy.]
 */
using UnityEngine;

public class WallTurret : MonoBehaviour
{
    ///public
    public bool isFiring;
    public float fireRate;
    public float bulletSpeed;
    public GameObject bulletPrefab;
    //getters for TurretBullet instantiation
    public Vector3 fireDirection { get { return _fireDirection; } }
    public float maxBulletDistance { get { return _maxBulletDistance; } }

    ///private
    private Vector3 _fireDirection;
    private GameObject _turretBarrel;
    private float _maxBulletDistance = 25f;

    void Awake()
    {
        //initialize values
        isFiring = true;

        //find barrel in children
        _turretBarrel = transform.GetChild(1).gameObject;
        _fireDirection = _turretBarrel.transform.position - transform.position;

        //get max bullet distance
        RaycastHit measure;
        //start point in front of object (otherwise raycast will detect itself)
        Vector3 raycastOrigin = transform.position + _fireDirection;
        if (Physics.Raycast(raycastOrigin, _fireDirection, out measure))
        {
            //calculate distance between
            _maxBulletDistance = Vector3.Distance(measure.point, transform.position);
        }

        if (isFiring) startShooting();
    }

    //call to start firing and change boolean value
    public void startShooting()
    {
        //loop
        InvokeRepeating("_shoot", 0f, fireRate);
        //set bool
        isFiring = true;
    }

    //call to stop firing and change boolean value
    public void ceaseFire()
    {
        //cancel loop
        CancelInvoke("_shoot");
        //set bool
        isFiring = false;
    }

    //general code to shoot that will be called through invoke repeating
    private void _shoot()
    {
        //get position and rotational values
        Vector3 spawnPos = _turretBarrel.transform.position;
        Quaternion zeroedQuat = new Quaternion(0f, 0f, 0f, 1f);

        //instantiate with NO parent to maintain scaling
        GameObject newBullet = Instantiate(bulletPrefab, spawnPos, zeroedQuat);
        //now parent the object
        newBullet.transform.parent = transform;
    }
}
