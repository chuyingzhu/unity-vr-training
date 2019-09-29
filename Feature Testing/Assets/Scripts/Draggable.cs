using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    public Transform target;
    Rigidbody rb;

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


    }
}
