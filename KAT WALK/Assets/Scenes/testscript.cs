using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript : MonoBehaviour
{
    float y = 0;
    int pos = 1;
    Quaternion newRotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        y +=pos ;
        if (y > 180)
            y = -179;
        newRotation = Quaternion.Euler(0.0f, y, 0);
        this.transform.rotation = newRotation;
        Debug.Log(this.transform.rotation);
    }
}
