using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool isOpen=false;

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
