using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ControllerEvents : MonoBehaviour {
    public Text msg;

    void OnCollisionEnter(Collision collisionInfo) {
        if (collisionInfo.collider.tag == "Flask") {
            FindObjectOfType<GameMan>().NextStep();
        }
    }

    /* 
    public void OnPickUp() {
        msg.text = "next step";
    }*/
}
