using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand : MonoBehaviour {
    // For pickup/drop objects
    public SteamVR_Action_Boolean m_GrabAction = null;
    public SteamVR_Action_Boolean m_UseAction = null;

    // Added to L/R controller game objects
    public SteamVR_Behaviour_Pose m_Pose = null;
    private FixedJoint m_Joint = null;

    // Current object that the controller is holding
    public Interactable m_CurrentInteractable = null;
    // List of stuff that the controller is touching
    public List<Interactable> m_ContactInteractables = new List<Interactable>();

    public GameObject otherController = null;
    public bool isFollowObject = false;
    public Transform followTarget = null;

    private void Awake() {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    private void Update() {
        // If grab button is pressed
        if (m_GrabAction.GetStateDown(m_Pose.inputSource)) {
            if (m_CurrentInteractable != null) {
                Drop();
                return;
            }
            Pickup();
        }

        /*
        if (m_GrabAction.GetStateUp(m_Pose.inputSource)) {
            print(m_Pose.inputSource + " Grab Up");
            Drop();
        }*/
        if (m_UseAction.GetStateDown(m_Pose.inputSource)) {
            print(m_Pose.inputSource + " Use Down");
            if (m_CurrentInteractable != null) {
                m_CurrentInteractable.Action();
            }
        }

        if (isFollowObject) {
            FollowObject(followTarget);
        }
    }

    // Called when controller collides with an object
    private void OnTriggerEnter(Collider other) {
        // If object is neither type "Interactable" or "Heavy", simply ignore it
        if (!other.gameObject.CompareTag("Interactable") && !other.gameObject.CompareTag("Heavy")) {
            return;
        }
        m_ContactInteractables.Add(other.gameObject.GetComponent<Interactable>());
        // Manage color
        if (other.gameObject.CompareTag("Heavy")) {
            // If the other controller is hovering over the same target
            if (otherController.GetComponent<Hand>().m_ContactInteractables.IndexOf(other.gameObject.GetComponent<Interactable>()) >= 0) {
                other.gameObject.GetComponent<ColorManager>().changeToGreen();
            }
        }
        else if (other.gameObject.CompareTag("Interactable")) {
            // As long as the other hand is not holding the same target, change to green
            if (otherController.GetComponent<Hand>().m_CurrentInteractable != other.gameObject.GetComponent<Interactable>()) {
                other.gameObject.GetComponent<ColorManager>().changeToGreen();
            }
        }
    }

    // Called when controller no longer collides with an object
    private void OnTriggerExit(Collider other) {
        // If object is neither type "Interactable" or "Heavy", simply ignore it
        if (!other.gameObject.CompareTag("Interactable") && !other.gameObject.CompareTag("Heavy")) {
            return;
        }
        m_ContactInteractables.Remove(other.gameObject.GetComponent<Interactable>());
        // Manage color
        // If both hands empty
        if (other.gameObject.CompareTag("Heavy")) {
            other.gameObject.GetComponent<ColorManager>().changeToBlack();
        }
        else if (other.gameObject.CompareTag("Interactable")) {
            // As long as the other hand is not holding the same target, change to green
            if (otherController.GetComponent<Hand>().m_CurrentInteractable != other.gameObject.GetComponent<Interactable>()) {
                other.gameObject.GetComponent<ColorManager>().changeToRed();
            }
        }
    }

    public void Pickup() {
        // Get nearest interactable
        m_CurrentInteractable = GetNearestInteractable();
        // Null check
        if (!m_CurrentInteractable) {
            return;
        }
        // Heavy obj check
        if (m_CurrentInteractable.gameObject.CompareTag("Heavy")) {
            // If the other hand is not hovering over the same obj
            if (otherController.GetComponent<Hand>().m_ContactInteractables.IndexOf(m_CurrentInteractable) < 0) {
                return;
            }
        }
        // Already held, check
        if (m_CurrentInteractable.m_ActiveHand && m_CurrentInteractable.gameObject.CompareTag("Interactable")) {
            m_CurrentInteractable.m_ActiveHand.Drop();
            m_CurrentInteractable.GetComponent<ColorManager>().changeToBlue();
        }
        // Position
        // m_CurrentInteractable.transform.position = transform.position;
        m_CurrentInteractable.ApplyOffset(transform);
        // Attach
        Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        m_Joint.connectedBody = targetBody;
        if (m_CurrentInteractable.gameObject.CompareTag("Heavy")) {
            otherController.GetComponent<Hand>().isFollowObject = true;
            otherController.GetComponent<Hand>().followTarget = m_CurrentInteractable.gameObject.transform;
        }
        // Set active hand
        m_CurrentInteractable.m_ActiveHand = this;
        // Change color
        m_CurrentInteractable.GetComponent<ColorManager>().changeToBlue();
    }

    public void Drop() {
        // Null check
        if (!m_CurrentInteractable) {
            return;
        }
        // Heavy obj check
        if (m_CurrentInteractable.gameObject.CompareTag("Heavy")) {
            // If the other hand is not holding grip
            if (!otherController.GetComponent<Hand>().m_GrabAction.GetStateDown(otherController.GetComponent<Hand>().m_Pose.inputSource)) {
                return;
            }
        }
        // Apply velocity
        Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        targetBody.velocity = m_Pose.GetVelocity();
        targetBody.angularVelocity = m_Pose.GetAngularVelocity();
        // Detach
        m_Joint.connectedBody = null;
        if (m_CurrentInteractable.gameObject.CompareTag("Heavy")) {
            otherController.GetComponent<Hand>().isFollowObject = true;
            otherController.GetComponent<Hand>().followTarget = null;
        }
        // Change color
        m_CurrentInteractable.GetComponent<ColorManager>().changeToRed();
        // Clear
        m_CurrentInteractable.m_ActiveHand = null;
        m_CurrentInteractable = null;
    }

    private Interactable GetNearestInteractable() {
        Interactable nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach(Interactable interactable in m_ContactInteractables) {
            distance = (interactable.transform.position - transform.position).sqrMagnitude;
            if (distance < minDistance) {
                minDistance = distance;
                nearest = interactable;
            }
        }

        return nearest;
    }

    private void FollowObject(Transform target) {
        transform.position = target.position;
    }
}