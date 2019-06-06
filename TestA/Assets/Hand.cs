using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand : MonoBehaviour {
    // For pickup/drop objects
    public SteamVR_Action_Boolean m_GrabAction = null;
    public SteamVR_Action_Boolean m_UseAction = null;

    // Added to L/R controller game objects
    private SteamVR_Behaviour_Pose m_Pose = null;
    private FixedJoint m_Joint = null;

    // Current object that the controller is holding
    private Interactable m_CurrentInteractable = null;
    // List of stuff that the controller is touching
    public List<Interactable> m_ContactInteractables = new List<Interactable>();

    public GameObject otherController = null;

    private void Awake() {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    private void Update() {
        // If grab button is pressed
        if (m_GrabAction.GetStateDown(m_Pose.inputSource)) {
            // Input source is L or R controller
            print(m_Pose.inputSource + " Grab Down");

            if (m_CurrentInteractable != null) {
                Drop();
                //m_CurrentInteractable.Action();
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
            if (m_CurrentInteractable != null)
            {
                m_CurrentInteractable.Action();
            }
        }
    }

    // Called when controller collides with an object
    private void OnTriggerEnter(Collider other) {
        // If object is not type "Interactable", simply ignore it
        if (!other.gameObject.CompareTag("Interactable")) {
            return;
        }
        m_ContactInteractables.Add(other.gameObject.GetComponent<Interactable>());
        // If this controller is not holding anything
        if (m_CurrentInteractable == null) {
            // If the other controller is hovering over the same target
            if (otherController.GetComponent<Hand>().m_ContactInteractables.IndexOf(other.gameObject.GetComponent<Interactable>()) >= 0) {
                other.gameObject.GetComponent<ColorManager>().changeToGreen();
            }
        }
    } 

    // Called when controller no longer collides with an object
    private void OnTriggerExit(Collider other) {
        // If object is not type "Interactable", simply ignore it
        if (!other.gameObject.CompareTag("Interactable")) {
            return;
        }
        m_ContactInteractables.Remove(other.gameObject.GetComponent<Interactable>());
        other.gameObject.GetComponent<ColorManager>().changeToRed();
    }

    public void Pickup() {
        // Get nearest interactable
        m_CurrentInteractable = GetNearestInteractable();
        // Null check
        if (!m_CurrentInteractable) {
            return;
        }
        // Already held, check
        if (m_CurrentInteractable.m_ActiveHand) {
            m_CurrentInteractable.m_ActiveHand.Drop();
        }
        // Position
        // m_CurrentInteractable.transform.position = transform.position;
        m_CurrentInteractable.ApplyOffset(transform);
        // Attach
        Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        m_Joint.connectedBody = targetBody;
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
        // Apply velocity
        Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        targetBody.velocity = m_Pose.GetVelocity();
        targetBody.angularVelocity = m_Pose.GetAngularVelocity();
        // Detach
        m_Joint.connectedBody = null;
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
}