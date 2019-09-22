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
      this.transform.localPosition += new Vector3(0.2f, 0, 0);
    }
}
