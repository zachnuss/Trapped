using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningItems : MonoBehaviour
{
    public bool timer = true;
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

<<<<<<< HEAD
        transform.Rotate(0, 0, 0.25f, Space.Self);
=======
        transform.Rotate(0, 0.25f, 0, Space.Self);
>>>>>>> cdddba93d7dc29d8a05f0fd9e0dc8304a011d6e6
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
