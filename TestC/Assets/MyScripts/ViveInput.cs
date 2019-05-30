using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ViveInput : MonoBehaviour {
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean teleportAction;
    public SteamVR_Action_Boolean grabAction;

    public bool GetTeleportDown() {
        return teleportAction.GetStateDown(handType);
    }

    public bool GetGrab() {
        return grabAction.GetState(handType);
    }

    void Update() {
        /*
        if (SteamVR_Actions._default.Teleport.GetStateDown(SteamVR_Input_Sources.Any)) {
            Debug.Log("TP down");
        }
        if (SteamVR_Actions._default.GrabPinch.GetStateUp(SteamVR_Input_Sources.Any)) {
            Debug.Log("Grab pinch up");
        }

        float triggerValue = squeezeAction.GetAxis(SteamVR_Input_Sources.Any);
        if (triggerValue > 0.0f) {
            Debug.Log(triggerValue);
        }

        Vector2 touchpadValue = touchPadAction.GetAxis(SteamVR_Input_Sources.Any);
        if (touchpadValue != Vector2.zero) {
            Debug.Log(touchpadValue);
        }
        */

        if (GetTeleportDown()) {
            print("Teleport " + handType);
        }

        if (GetGrab()) {
            print("Grab " + handType);
        }
    }
}