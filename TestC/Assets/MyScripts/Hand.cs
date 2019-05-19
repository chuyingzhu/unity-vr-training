using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand : MonoBehaviour {
    // For pickup/drop objects
    public SteamVR_Action_Boolean m_GrabAction = null;
    // Added to L/R controller game objects
    private SteamVR_Behaviour_Pose m_Pose = null;
    private FixedJoint m_Joint = null;
    // Current object that the controller is holding
    private Interactable m_CurrentInteractable = null;
    // List of stuff that the controller is touching
    public List<Interactable> m_ContactInteractables = new List<Interactable>();

    private void Awake() {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    private void Update() {
        // Down
        if (m_GrabAction.GetStateDown(m_Pose.inputSource)) {
            // Input source is L or R controller
            Debug.Log(m_Pose.inputSource + " Trigger Down");
            Pickup();
        }

        // Up
        if (m_GrabAction.GetStateUp(m_Pose.inputSource)) {
            Debug.Log(m_Pose.inputSource + " Trigger Up");
            Drop();
        }
    }

    private void OnTriggerEnter(Collider other) {
        // If object is not type "Interactable", simply ignore it
        if (!other.gameObject.CompareTag("Interactable")) {
            return;
        }

        m_ContactInteractables.Add(other.gameObject.GetComponent<Interactable>());
    } 

    private void OnTriggerExit(Collider other) {
        // If object is not type "Interactable", simply ignore it
        if (!other.gameObject.CompareTag("Interactable")) {
            return;
        }

        m_ContactInteractables.Remove(other.gameObject.GetComponent<Interactable>());
    }

    public void Pickup() {

    }

    public void Drop() {

    }

    private Interactable GetNearestInteractable() {
        return null;
    }
}