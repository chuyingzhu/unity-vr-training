using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    bool isOpen=false;
    bool opening=false;
    bool closing=false;
    float rotateDegree=0.0f;
 
    void Update(){
        if(opening) {
            print("opening");
            transform.Rotate(0,1,0);
            rotateDegree += (1);
            if(rotateDegree>110) {
                opening =false;
                isOpen=true;
            }
        }
        if(closing){
            print("closing");
            transform.Rotate(0,-1,0);
            rotateDegree-=(1);
            if(rotateDegree<0){
                closing=false;
                isOpen=false;
            }
        }
    }

    public void Open(){
        print("click");
        if(!isOpen){
            opening=true;
            print("open");
        }
        else {
            closing=true;
            print("close");
        }

    }
}
