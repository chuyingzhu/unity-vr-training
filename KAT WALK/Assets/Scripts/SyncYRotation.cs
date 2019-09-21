using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncYRotation : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.eulerAngles = new Vector3(0, target.transform.rotation.y, 0);
      //  Quaternion x=new Quaternion.euler
    }
}
