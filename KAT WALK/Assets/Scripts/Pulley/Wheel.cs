using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    private Rigidbody rb;

    public bool wheelFrontLeft;
    public bool wheelFrontRight;
    public bool wheelRearLeft;
    public bool wheelRearRight;

    [Header("Suspension")]
    public float restLength;
    public float springTravel;
    public float springStiffness;
    public float damperStiffness;

    private float minLength;
    private float maxLength;
    private float lastLength;
    private float springLength;
    private float springForce;
    private float springVelocity;
    private float damperForce; 

    private Vector3 suspensionForce;

    [Header("Wheel")]
    public float wheelRadius;
    private Vector3 wheelVelocityLS; //Locol space
    private float Fx;
    private float Fy;


    void Start()
    {
        // For the physics
        rb = transform.root.GetComponent<Rigidbody>();

        minLength = restLength - springTravel;
        maxLength = restLength + springTravel;
    }

    void Update()
    {
        Debug.DrawRay(transform.position, -transform.up * (springLength + wheelRadius), Color.green);
    }


    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, maxLength + wheelRadius))
        {
            lastLength = springLength;
            springLength = hit.distance - wheelRadius;
            springLength = Mathf.Clamp(springLength, minLength, maxLength);
            springVelocity = (lastLength - springLength) / Time.fixedDeltaTime;
            springForce = springStiffness * (restLength - springLength);
            damperForce = damperStiffness * springVelocity;
            suspensionForce = (springForce + damperForce) * transform.up;
            wheelVelocityLS = transform.InverseTransformDirection(rb.GetPointVelocity(hit.point)); // Making sure that the calculation is based on the raycast hit point
            Fx = Input.GetAxis("Vertical") * springForce;
            Fy = wheelVelocityLS.x * springForce;
            rb.AddForceAtPosition(suspensionForce + (Fx * transform.forward) + (Fy * -transform.right), hit.point);  

            
        }
    }
}
