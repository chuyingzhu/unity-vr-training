using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    bool isOpen=false;
    bool open=false;
    bool close=false;
    int rotateDegree=0;
    
    void Update(){
        if(open==true){
            print("opening");
                transform.Rotate(0,10*Time.deltaTime,0);
                rotateDegree+=10;
                if(rotateDegree>=110){
                    open=false;
                    isOpen=true;
                }
            
        }
        if(close==true){
            print("closing");
                transform.Rotate(0,-10*Time.deltaTime,0);
                rotateDegree-=10;
                if(rotateDegree<=0){
                    close=false;
                    isOpen=false;
                }
            
        }
    }

    public void Open(){
        print("click");
        if(!isOpen){
            open=true;
            print("open");
        }
        else {
            close=true;
            print("close");
        }

    }
}
