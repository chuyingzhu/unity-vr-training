﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugging_PC : MonoBehaviour
{
    Rigidbody rb;   
    public float Force;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null)
                {
                    if (rb = hit.transform.GetComponent<Rigidbody>())
                    {
                        PrintName(hit.transform.gameObject);
                        AddForce();
                    }
                }
            }
        }
    }

    void PrintName(GameObject obj)
    {
        print(obj.name);
    }

    void AddForce()
    {
        rb.AddForce(transform.forward * Force, ForceMode.Impulse);
    }

}