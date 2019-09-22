using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncYRotation : MonoBehaviour
{
    public GameObject target;
    Quaternion newRotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        newRotation = Quaternion.Euler(0, target.transform.eulerAngles.y, 0);
        this.transform.rotation = newRotation;
        this.transform.position = new Vector3(target.transform.position.x, this.transform.position.y, target.transform.position.z);
       // this.transform.rotation = Quaternion.Euler(0, target.transform.rotation.y, 0);
       // this.transform.eulerAngles = new Vector3(0, target.transform.localRotation.y, 0);
        Debug.Log("model rotationy: "+this.transform.rotation.y+" eulery: "+this.transform.eulerAngles.y);
        Debug.Log("target rotationy: " + target.transform.rotation.y + " eulery: " + target.transform.eulerAngles.y);
        //  Quaternion x=new Quaternion.euler
    }
}
