
using System;
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

    public Player m_Player;
    public Animator m_Animator;

    private string[] tags = new string [6] {"Interactable", "Heavy", "Tyvex", "Flask","trolley", "OpenButton"};

    // How much touchpad value affects speed (-1 to 1)
    public float m_Sensitivity = 0.1f;
    public float m_MaxSpeed = 1.0f;
    public SteamVR_Action_Boolean m_MovePress = null;
    public SteamVR_Action_Vector2 m_MoveValue = null;
    public Transform m_CameraRigTransform = null;
    public Camera m_Camera = null;

    //body and tyvex model
    public GameObject nakedModel;
    public GameObject tyvexModel;

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
            //print(m_Pose.inputSource + " Use Down");
            if (m_CurrentInteractable != null) {
                m_CurrentInteractable.Action();
            }
        }
    }

    // Called when controller collides with an object
    private void OnTriggerEnter(Collider other) {
        // If object's tag is not in the "tags" array, simply ignore it
        if (!Array.Exists(tags, element => element == other.gameObject.tag)) {
            return;
        }
        m_ContactInteractables.Add(other.gameObject.GetComponent<Interactable>());
        // Manage color
        if (other.gameObject.CompareTag("Heavy")) {
            // If the other controller is hovering over the same target
            if (otherController.GetComponent<Hand>().m_ContactInteractables.Contains(other.gameObject.GetComponent<Interactable>())) {
                other.gameObject.GetComponent<ColorManager>().changeToGreen();
            }
        }
        else if (other.gameObject.CompareTag("Interactable") || other.gameObject.CompareTag("Flask")) {
            // As long as the other hand is not holding the same target, change to green
            if (otherController.GetComponent<Hand>().m_CurrentInteractable != other.gameObject.GetComponent<Interactable>()) {
                other.gameObject.GetComponent<ColorManager>().changeToGreen();
            }
        }
        // Simply change to green
        else {
        	other.gameObject.GetComponent<ColorManager>().changeToGreen();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Door")
        {

            if (m_UseAction.GetStateDown(m_Pose.inputSource))
            {
                other.gameObject.GetComponentInParent<Door>().Open();
                if (m_Player.currentStep == 2)
                {
                    m_Player.NextStep();
                }
            }

        }
        if (other.gameObject.tag == "OpenButton")
        {
            
            if (m_UseAction.GetStateDown(m_Pose.inputSource))
            {
                Debug.Log("OpenButton Clicked");
                other.gameObject.GetComponentInParent<AirLockDoor>().Open();
                if (m_Player.currentStep == 7)
                {
                    m_Player.NextStep();
                }
            }

        }
        if (other.gameObject.tag == "closet")
        {
            if (m_UseAction.GetStateDown(m_Pose.inputSource))
            {
                nakedModel.SetActive(!nakedModel.activeSelf);
            tyvexModel.SetActive(!tyvexModel.activeSelf);
                if (m_Player.currentStep == 3)
                {
                    m_Player.NextStep();
                }
            }
        }
        if (other.gameObject.tag == "Flask")
        {

            if (m_UseAction.GetStateDown(m_Pose.inputSource))
            {
                Debug.Log("Flask Clicked");
                Pickup();
            }

        }
        if (other.gameObject.tag == "trolley")
        {

            if (m_UseAction.GetStateDown(m_Pose.inputSource))
            {
                Debug.Log("trolley Clicked");
                Pickup();
            }

        }
    }
    // Called when controller no longer collides with an object
    private void OnTriggerExit(Collider other) {
        // If object's tag is not in the "tags" array, simply ignore it
        if (!Array.Exists(tags, element => element == other.gameObject.tag)) {
            return;
        }
        m_ContactInteractables.Remove(other.gameObject.GetComponent<Interactable>());
        if (other.gameObject.CompareTag("Heavy")) {
            other.gameObject.GetComponent<ColorManager>().changeToBlack();
        }
        else if (other.gameObject.CompareTag("Interactable")) {
            // As long as the other hand is not holding or hovering over the same target, change to green
            if ((!otherController.GetComponent<Hand>().m_ContactInteractables.Contains(other.gameObject.GetComponent<Interactable>())) && (otherController.GetComponent<Hand>().m_CurrentInteractable != other.gameObject.GetComponent<Interactable>())) {
                other.gameObject.GetComponent<ColorManager>().changeToRed();
            }
        }
        else if (other.gameObject.CompareTag("Flask")) {
            // As long as the other hand is not holding or hovering over the same target, change to clear
            if ((!otherController.GetComponent<Hand>().m_ContactInteractables.Contains(other.gameObject.GetComponent<Interactable>())) && (otherController.GetComponent<Hand>().m_CurrentInteractable != other.gameObject.GetComponent<Interactable>())) {
                other.gameObject.GetComponent<ColorManager>().changeToClear();
            }
        }
        else if (other.gameObject.CompareTag("Tyvex")) {
            // As long as the other hand is not hovering over the same target, change to gray
            if (!otherController.GetComponent<Hand>().m_ContactInteractables.Contains(other.gameObject.GetComponent<Interactable>())) {
                other.gameObject.GetComponent<ColorManager>().changeToGray();
            }
        }
        else if (other.gameObject.CompareTag("OpenButton")) {
             // As long as the other hand is not hovering over the same target, change to red
             if (!otherController.GetComponent<Hand>().m_ContactInteractables.Contains(other.gameObject.GetComponent<Interactable>())) {
                other.gameObject.GetComponent<ColorManager>().changeToRed();
            }
        }
        else if (other.gameObject.CompareTag("CloseButton")) {
             // As long as the other hand is not hovering over the same target, change to red
             if (!otherController.GetComponent<Hand>().m_ContactInteractables.Contains(other.gameObject.GetComponent<Interactable>())) {
                other.gameObject.GetComponent<ColorManager>().changeToRed();
            }
        }
        else if (other.gameObject.CompareTag("trolley"))
        {
            if (!otherController.GetComponent<Hand>().m_ContactInteractables.Contains(other.gameObject.GetComponent<Interactable>()))
                other.gameObject.GetComponent<ColorManager>().changeToRed();
        }
    }

    public void Pickup() {
        // Get nearest interactable
        m_CurrentInteractable = GetNearestInteractable();
        // Null check
        if (!m_CurrentInteractable) {
            return;
        }
        //Already held check
        if (m_CurrentInteractable.m_ActiveHand)
        {
            m_CurrentInteractable.m_ActiveHand.Drop();
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
        // Tyvex
        if (m_CurrentInteractable.gameObject.CompareTag("Tyvex")) {
            m_CurrentInteractable.gameObject.SetActive(false);
          //  m_Player.NextStep();
            return;
        }
        // OpenButton
        if (m_CurrentInteractable.gameObject.CompareTag("OpenButton")) {
            m_CurrentInteractable.gameObject.SetActive(false);
            // Play animation
            m_Animator.Play("Open", -1, 0f);
           // m_Player.NextStep();
            return;
        }
        // CloseButton
        if (m_CurrentInteractable.gameObject.CompareTag("CloseButton")) {
            m_CurrentInteractable.gameObject.SetActive(false);
            // Reverse animation play
            m_Animator.SetFloat("Direction", -1.0f);
            m_Animator.Play("Open", -1, float.NegativeInfinity);
           // m_Player.NextStep();
            return;
        }
        // Position
        // m_CurrentInteractable.transform.position = transform.position;
        if (m_CurrentInteractable.gameObject.CompareTag("trolley"))
        {
            m_CurrentInteractable.ApplyOffsetT(transform);
        }
        else
        {
            m_CurrentInteractable.ApplyOffset(transform);
        }
        // Attach
        Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        m_Joint.connectedBody = targetBody;
        if (m_CurrentInteractable.gameObject.CompareTag("Heavy")) {
            otherController.GetComponent<Hand>().m_Pose.enabled = false;
        }
        // Set active hand
        m_CurrentInteractable.m_ActiveHand = this;
        // Change color
        if (m_CurrentInteractable.gameObject.CompareTag("Flask")) {
            m_CurrentInteractable.GetComponent<ColorManager>().changeToClear();
        }
        else {
            m_CurrentInteractable.GetComponent<ColorManager>().changeToBlue();
        }
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

    // FOR TOUCHPAD MOVEMENT
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