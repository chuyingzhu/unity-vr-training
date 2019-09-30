using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform target;
    public Transform handle;
    public float force;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(target.position);

        AddForcetoCart();
    }

    void AddForcetoCart()
    {
        // Adding force to the cart
        var heading = target.position - handle.position;
        var distance = heading.magnitude;
        var direction = heading / distance;
        print(direction.z); //For Debugging
        if (float.IsNaN(direction.z) || direction.z == 0.0)
        {
         //   print("No interaction");
        }
        else
        {
            rb.AddForce(transform.forward * (-direction.z * force));
        }
    }
}
