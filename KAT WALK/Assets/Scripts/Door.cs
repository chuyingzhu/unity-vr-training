using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    bool isOpen=false;
    bool opening=false;
    bool closing=false;
    float rotateDegree=0.0f;
    void Start(){
        transform.Rotate(0,90,0);
        transform.Rotate(0,90,0);
    }
    void Update(){
        if(opening) {
            print("opening");
            transform.Rotate(0,10*Time.deltaTime,0);
            rotateDegree += (10*Time.deltaTime);
            if(rotateDegree>110) {
                opening =false;
                isOpen=true;
            }
        }
        if(closing){
            print("closing");
            transform.Rotate(0,-10*Time.deltaTime,0);
            rotateDegree-=(10*Time.deltaTime);
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
