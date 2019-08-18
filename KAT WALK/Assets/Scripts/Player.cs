using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public List<GameObject> m_Markers = new List<GameObject>();
	private int current = 0;

	private void Awake() {
        for (int i=1; i<m_Markers.Count; i++) {
        	m_Markers[i].active = false;
        }
    }

    // Called when player collides with an object
    private void OnTriggerEnter(Collider other) {
        // If object is not the type "Marker" simply ignore it
        if (!other.gameObject.CompareTag("Marker")) {
            return;
        }
        print("Marker detected");
		other.gameObject.GetComponent<ColorManager>().changeToGreen();
		other.enabled = false;
		if (current < m_Markers.Count) {
			m_Markers[++current].active = true;
		}
    }
}