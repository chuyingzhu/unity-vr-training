using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonTrigger : MonoBehaviour
{
    // Event for when triggered
    public UnityEvent TriggerHit;

    // Detecting if a GameObj with trigger has collided
    void OnTriggerEnter(Collider obj)
    {
        TriggerHit.Invoke();
    }

    // Make a desired event, HAS TO BE PUBLIC
    public void Print(string text)
    {
        print(text);
    } 
}
