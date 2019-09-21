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

    void Update()
    {
        if (opening)
        {
            print("opening");
            leftDoor.transform.Translate(-0.2f, 0, 0);
            rightDoor.transform.Translate(0.2f, 0, 0);
            distance += (0.02f);
            if (distance > 2.6)
            {
                opening = false;
                isOpen = true;
            }
        }
        if (closing)
        {
            print("closing");
            leftDoor.transform.Translate(0.2f, 0, 0);
            rightDoor.transform.Translate(0.2f, 0, 0);
            distance -= (0.02f);
            if (distance <= 2.6)
            {
                closing = false;
                isOpen = false;
            }
        }
    }

    public void Open()
    {
        print("click");
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
