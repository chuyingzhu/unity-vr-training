using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerGrabObject : MonoBehaviour {
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean grabAction;
    // Stores the GameObject that the trigger is currently colliding with,
    // so you have the ability to grab the object.
    private GameObject collidingObject;
    // A reference to the GameObject that the player is currently grabbing.
    private GameObject objectInHand;

    private void SetCollidingObject(Collider col) {
        // If the player is already holding something or the object has no rigidbody.
        if (collidingObject || !col.GetComponent<Rigidbody>()) {
            return;
        }
        collidingObject = col.gameObject;
    }

    // When the trigger collider enters another,
    // this sets up the other collider as a potential grab target.
    public void OnTriggerEnter(Collider other) {
        SetCollidingObject(other);
        // Debug
        print("Trigger just entered a collider");
    }

    // Ensures that the target is set when the player holds
    // a controller over an object for a while.
    public void OnTriggerStay(Collider other) {
        SetCollidingObject(other);
        // Debug
        print("Trigger is staying on a collider");
    }

    // When the collider exits an object, abandoning an ungrabbed target,
    // this code removes its target.
    public void OnTriggerExit(Collider other) {
        if (!collidingObject) {
            return;
        }
        collidingObject = null;
        // Debug
        print("Trigger just left a collider");
    }

    private void GrabObject() {
        // Move the colliding GameObject into the player’s hand and
        // remove it from the collidingObject variable.
        objectInHand = collidingObject;
        collidingObject = null;
        // Add a new joint that connects the controller to the object.
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    // Make a new fixed joint, add it to the controller,
    // and then set it up so it doesn’t break easily.
    private FixedJoint AddFixedJoint() {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }
    
    private void ReleaseObject() {
        // Make sure there’s a fixed joint attached to the controller.
        if (GetComponent<FixedJoint>()) {
            // Remove the connection to the object held by the joint and destroy the joint.
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            // Add the speed and rotation of the controller when the player releases the object,
            // so the result is a realistic arc.
            objectInHand.GetComponent<Rigidbody>().velocity = controllerPose.GetVelocity();
            objectInHand.GetComponent<Rigidbody>().angularVelocity = controllerPose.GetAngularVelocity();
        }
        // Remove the reference to the formerly attached object.
        objectInHand = null;
    }


    // Update is called once per frame
    void Update() {
        // When the player triggers the Grab action, grab the object.
        if (grabAction.GetLastStateDown(handType)) {
            if (collidingObject) {
                GrabObject();
            }
        }

        // If the player releases the input linked to the Grab action
        // and there’s an object attached to the controller, this releases it.
        if (grabAction.GetLastStateUp(handType)) {
            if (objectInHand) {
                ReleaseObject();
            }
        }
    }
}