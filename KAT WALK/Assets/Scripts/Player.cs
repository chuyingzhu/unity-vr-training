using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour {
	public List<GameObject> m_Markers = new List<GameObject>();
    public TextMeshPro label;
    private string[] steps = new string [3] {"Welcome. Please Walk to the door.",
                                            "Good job! Next, walk to the change room.",
                                            "Great! Now grab a Tyvex."};
	private int current = 0;

	private void Awake() {
        for (int i=1; i<m_Markers.Count; i++) {
        	m_Markers[i].SetActive(false);
        }
        label.text = steps[0];
    }

    // Called when player collides with an object
    private void OnTriggerEnter(Collider other) {
        // If object is not the type "Marker" simply ignore it
        if (!other.gameObject.CompareTag("Marker")) {
            return;
        }
		other.gameObject.GetComponent<ColorManager>().changeToGreen();
		other.enabled = false;
		if (++current < m_Markers.Count) {
			m_Markers[current].active = true;
            label.text = steps[current];
		}
    }
}