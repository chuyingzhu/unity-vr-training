using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool isOpen=false;
    // Start is called before the first frame update
    void Start()
    {
         
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open(){
        print("click");
        if(!isOpen){
            transform.Rotate(0,110,0);
            isOpen=true;
        }
        else {
            transform.Rotate(0,-110,0);
            isOpen=false;
        }

    }
}
