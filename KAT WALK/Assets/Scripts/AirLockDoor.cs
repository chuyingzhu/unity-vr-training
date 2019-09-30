using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirLockDoor : MonoBehaviour
{
    public GameObject leftDoor, rightDoor;
    bool isOpen = false;
    bool opening = false;
    bool closing = false;
    float distance = 0.0f;
    public float length;
    void Update()
    {
        if (opening)
        {
           
            
            print("opening");
            leftDoor.transform.localPosition += new Vector3(-0.1f, 0, 0);
            rightDoor.transform.localPosition += new Vector3(+0.1f, 0, 0);
            //leftDoor.transform.Translate(-0.2f, 0, 0);
            //rightDoor.transform.Translate(0.2f, 0, 0);
            distance += (0.1f);
            Debug.Log(leftDoor.transform.localPosition+" "+rightDoor.transform.localPosition);
            if (distance >= length)
            {
                opening = false;
                isOpen = true;
                distance = 0;
            }
        }
        if (closing)
        {
            print("closing");
            leftDoor.transform.localPosition += new Vector3(+0.2f, 0, 0);
            rightDoor.transform.localPosition += new Vector3(-0.2f, 0, 0);
           // leftDoor.transform.Translate(0.2f, 0, 0);
            //rightDoor.transform.Translate(-0.2f, 0, 0);
            distance += (0.1f);
            Debug.Log(distance);
            if (distance >= length)
            {
                closing = false;
                isOpen = false;
                distance = 0;
            }
        }
    }

    public void Open()
    {
        print("click");
        if ((!opening) && (!closing))
        {
            if (!isOpen)
            {
                opening = true;
                print("open");
            }
            else
            {
                closing = true;
                print("close");
            }
        }
    }
}
