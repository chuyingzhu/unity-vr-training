using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabHandleBar : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    public Transform target;
    public Transform handle;
    public Transform Cart;
    Rigidbody rb, rb1;

    private void Update()
    {
        // If hands move too far away, auto ungrip
        if(Vector3.Distance(target.position, transform.position) > 20.0f)
        {
            OnMouseUp();
        }
    }

    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // Store offset
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        // Pixel coor. (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coor of gameObject on screen
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        // Allow dragging of obj
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
    }

}
