using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum powerUpType
{
    damage,
    speed,
    health
};

public class PowerUpDrop : MonoBehaviour
{
    [Header("What type of powerup")]
    public powerUpType type;


    public bool timer = true;
    [Header("How long powerup will last")]
    public int powerUpDuration = 10;

    bool yDir = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ymove());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      //  if (arrowOn)
       // {
            if (yDir)
                transform.localPosition += new Vector3(0, 0.25f * Time.deltaTime, 0);
            else
                transform.localPosition += new Vector3(0, -0.25f * Time.deltaTime, 0);

            transform.Rotate(0, 0.25f, 0, Space.Self);
       // }
    }

    //move in y dir
    IEnumerator ymove()
    {
        yield return new WaitForSeconds(1.0f);
        if (yDir)
            yDir = false;
        else
            yDir = true;

        StartCoroutine(ymove());

    }
}
