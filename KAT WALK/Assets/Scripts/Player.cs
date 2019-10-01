using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour {
	public List<GameObject> m_Markers = new List<GameObject>();
    public TextMeshPro label;
    public GameObject uiText;
    public TextMeshProUGUI uiLabel;
    public GameObject Quiz;
    private string[] steps = new string [9] {"Welcome. Please Walk to the door.0",
                                            "Good job! Next, walk to the change room.1",
                                            "Open the door.2",
                                            "Great! Now grab a Tyvex.3",
                                            "Walk out of the door.4",
                                            "Walk to the next marker.5",
                                            "Push the trolley to the end of the hall.6",
                                        	"Push the button to open door.7",
                                            "Push the button to close door.8"};
	public int currentStep = 0;
    public int currentMarker = 0;
    // 1 means the step has a marker, 0 means it does not
    // Step[2] has a temp marker, will use a real doorknob later
    private int[] stepInfo = new int [10] {1, 1, 1, 0, 1, 1, 1, 1, 0, 0};

	private void Awake() {
        for (int i=1; i<m_Markers.Count; i++) {
        	m_Markers[i].SetActive(false);
        }
        label.text = steps[0]+" step "+currentStep;
       // uiLabel = uiText.GetComponent<TextMeshProUGUI>();
        uiLabel.text= steps[0];
        currentStep = 0;
        currentMarker = 0;
    }

    // Called when player collides with an object
    private void OnCollisionEnter(Collision coll) {
        // If object is not the type "Marker" simply ignore it
        //if (!coll.gameObject.CompareTag("Marker")) {
           // print("not valid collision");
       //     return;
       // }
        if (coll.gameObject.tag == "Marker")
        {
            print("collision with "+coll.gameObject.name);
            coll.gameObject.GetComponent<ColorManager>().changeToGreen();
            coll.gameObject.SetActive(false);
            NextStep();
        }
       
    }
    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Autodoor")
        {
            print("exiting " + coll.gameObject.name);
            coll.gameObject.GetComponentInParent<AirLockDoor>().Open();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Marker")
        {
            print("collision with " + other.gameObject.name);
            other.gameObject.GetComponent<ColorManager>().changeToGreen();
            other.gameObject.SetActive(false);
            NextStep();
        }
        if (other.gameObject.tag == "Autodoor")
        {
            print("collision with " + other.gameObject.name);
            other.gameObject.GetComponentInParent<AirLockDoor>().Open();
        }
    }
    public void NextStep() {
        // Index range check
        Debug.Log("step " + currentStep);
        currentStep++;
        if (currentStep < steps.Length) {
            // If the upcoming step has a marker
            if (stepInfo[currentStep] == 1) {
                currentMarker++;
                m_Markers[currentMarker].SetActive(true);
            }
            label.text = steps[currentStep];
            uiLabel.text = steps[currentStep];
        }
        if (currentStep == steps.Length)
        {
            Quiz.SetActive(true);
            uiLabel.gameObject.SetActive(false);
        }
    }
}