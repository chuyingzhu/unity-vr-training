using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GrabHandleBar : MonoBehaviour
{
    // PC Debugging only
    /*private Vector3 mOffset;
    private float mZCoord;*/
    public Transform target;
    public Transform handle;
    public Transform Cart;
    public SteamVR_Action_Boolean grabPinch;
    public SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.Any;
    Rigidbody rb, rb1;

    private void FixedUpdate()
    {
        // If hands move too far away, auto ungrip
        if(Vector3.Distance(target.position, transform.position) > 1.0f)
        {
            // OnMouseUp(); //PC Debugging only
            ReturntoInitPos();
        }
        if (grabPinch.GetStateUp(inputSource))
        {
            ReturntoInitPos();
        }
    }


    private void ReturntoInitPos()
    {
        // Returning to original position
        transform.position = target.position;
        transform.rotation = target.rotation;

        // Resetting object's velocity
        rb = target.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb1 = Cart.GetComponent<Rigidbody>();
        var heading = target.position - handle.position;
        var distance = heading.magnitude;
        var direction = heading / distance;
        if (float.IsNaN(direction.z) || direction.z == 0.0)
        {
            rb1.velocity = Vector3.zero;
            rb1.angularVelocity = Vector3.zero;
        }
    }

    // PC Debugging only
    /*void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        // Store offset
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }
    
    private Vector3 GetMouseWorldPos()
    {
        // Pixel coord.
        Vector3 mousePoint = Input.mousePosition;

        //z coord. of gameObject on screen
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }

    private void OnMouseUp()
    {
        // Returning to original position
        transform.position = target.position;
        transform.rotation = target.rotation;

        // Resetting object's velocity
        rb = target.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb1 = Cart.GetComponent<Rigidbody>();
        var heading = target.position - handle.position;
        var distance = heading.magnitude;
        var direction = heading / distance;
        if (float.IsNaN(direction.z) || direction.z == 0.0)
        {
            rb1.velocity = Vector3.zero;
            rb1.angularVelocity = Vector3.zero;
        }
    }*/
    

}
