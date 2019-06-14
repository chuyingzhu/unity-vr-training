﻿using System.Collections;
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

    // How much touchpad value affects speed (-1 to 1)
    public float m_Sensitivity = 0.1f;
    public float m_MaxSpeed = 1.0f;
    public SteamVR_Action_Boolean m_MovePress = null;
    public SteamVR_Action_Vector2 m_MoveValue = null;
    public Transform m_CameraRigTransform = null;
    public Camera m_Camera = null;

    private float m_Speed = 0.0f;

    private void Awake() {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    private void Update() {
        CalculateMovement();
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
    }

    // Called when controller collides with an object
    private void OnTriggerEnter(Collider other) {
        // If object is neither type "Interactable" or "Heavy", simply ignore it
        if (!other.gameObject.CompareTag("Interactable") && !other.gameObject.CompareTag("Heavy") && !other.gameObject.CompareTag("Flask")) {
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
        if (!other.gameObject.CompareTag("Interactable") && !other.gameObject.CompareTag("Heavy") && !other.gameObject.CompareTag("Flask")) {
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
            otherController.GetComponent<Hand>().m_Pose.enabled = false;
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
        /*
        // Heavy obj check
        if (m_CurrentInteractable.gameObject.CompareTag("Heavy")) {
            // If the other hand is not holding grip
            if (!otherController.GetComponent<Hand>().m_GrabAction.GetStateDown(otherController.GetComponent<Hand>().m_Pose.inputSource)) {
                return;
            }
        }*/
        // Apply velocity
        Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        targetBody.velocity = m_Pose.GetVelocity();
        targetBody.angularVelocity = m_Pose.GetAngularVelocity();
        // Detach
        m_Joint.connectedBody = null;
        if (m_CurrentInteractable.gameObject.CompareTag("Heavy")) {
            otherController.GetComponent<Hand>().m_Pose.enabled = true;
        }
        // Change color
        // m_CurrentInteractable.GetComponent<ColorManager>().changeToRed();
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

    private void CalculateMovement() {
        // Figure out movement orientation
        Vector3 orientationEuler = new Vector3(0, m_Camera.transform.eulerAngles.y, 0);
        Quaternion orientation = Quaternion.Euler(orientationEuler);
        Vector3 movement = Vector3.zero;
        // If not moving, stop immediately
        if (m_MovePress.GetStateUp(SteamVR_Input_Sources.Any)) {
            m_Speed = 0;
        }
        // If button pressed
        if (m_MovePress.state) {
            // Add, clamp
            m_Speed += m_MoveValue.axis.y * m_Sensitivity;
            m_Speed = Mathf.Clamp(m_Speed, -m_MaxSpeed, m_MaxSpeed);
            // Orientation
            movement += orientation * (m_Speed * Vector3.forward) * Time.deltaTime;
        }
        // Apply
        m_CameraRigTransform.Translate(movement);
    }
}