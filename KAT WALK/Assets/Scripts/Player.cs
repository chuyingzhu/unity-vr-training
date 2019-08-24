using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour {
	public List<GameObject> m_Markers = new List<GameObject>();
    public TextMeshPro label;
    private string[] steps = new string [10] {"Welcome. Please Walk to the door.",
                                            "Good job! Next, walk to the change room.",
                                            "Open the door.",
                                            "Great! Now grab a Tyvex.",
                                            "Walk out of the door.",
                                            "Walk to the reception table.",
                                            "Walk to the indicated door.",
                                            "Open the door.",
                                        	"Push the button to open door.",
                                            "Push the button to close door."};
	private int currentStep = 0;
    private int currentMarker = 0;
    // 1 means the step has a marker, 0 means it does not
    // Step[2] has a temp marker, will use a real doorknob later
    private int[] stepInfo = new int [10] {1, 1, 1, 0, 1, 1, 1, 1, 0, 0};

	private void Awake() {
        for (int i=1; i<m_Markers.Count; i++) {
        	m_Markers[i].SetActive(false);
        }
        label.text = steps[0];
    }

    // Called when player collides with an object
    private void OnCollisionEnter(Collision coll) {
        // If object is not the type "Marker" simply ignore it
        if (!coll.gameObject.CompareTag("Marker")) {
            return;
        }
        print("collision");
		coll.gameObject.GetComponent<ColorManager>().changeToGreen();
		coll.gameObject.SetActive(false);
        NextStep();
    }

    public void NextStep() {
        // Index range check
        if (++currentStep < steps.Length) {
            // If the upcoming step has a marker
            if (stepInfo[currentStep] == 1) {
                m_Markers[++currentMarker].SetActive(true);
            }
            label.text = steps[currentStep];
        }
    }
}