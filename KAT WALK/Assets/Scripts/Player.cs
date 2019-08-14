using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // Called when controller collides with an object
    private void OnTriggerEnter(Collider other) {
        // If object is not the type "Marker" simply ignore it
        if (!other.gameObject.CompareTag("Marker")) {
            return;
        }
        other.gameObject.GetComponent<ColorManager>().changeToGreen();
    }
}
