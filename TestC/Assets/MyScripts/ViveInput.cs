using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ViveInput : MonoBehaviour {
    [SteamVR_DefaultAction("Squeeze")]

    public SteamVR_Action_Single squeezeAction;
    public SteamVR_Action_Vector2 touchPadAction;

     // Update is called once per frame
    void Update() {
        /* 
        if (SteamVR_Actions._default.Teleport.GetStateDown(SteamVR_Input_Sources.Any)) {
            Debug.Log("TP down");
        }
        if (SteamVR_Actions._default.GrabPinch.GetStateUp(SteamVR_Input_Sources.Any)) {
            Debug.Log("Grab pinch up");
        }*/


        float triggerValue = squeezeAction.GetAxis(SteamVR_Input_Sources.Any);
        if (triggerValue > 0.0f) {
            Debug.Log(triggerValue);
        }

        Vector2 touchpadValue = touchPadAction.GetAxis(SteamVR_Input_Sources.Any);
        if (touchpadValue != Vector2.zero) {
            Debug.Log(touchpadValue);
        }
    }
}