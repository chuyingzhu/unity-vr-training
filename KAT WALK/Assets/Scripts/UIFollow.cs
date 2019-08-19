using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIFollow : MonoBehaviour {
	public TextMeshPro text;
	public Vector3 offset;
	public Camera m_Camera;

    // Debugging purpose
    void Start() {
        transform.localScale = new Vector3(-1.0f, 0, 0);
    }

    // Update is called once per frame
    void Update() {
        text.transform.position = this.transform.position + offset;
        // Flip the text if head turns around
        if (m_Camera.transform.localEulerAngles.y > 90 || m_Camera.transform.localEulerAngles.y < -90) {
            print("Head flipped");
        	transform.localScale = new Vector3(-1.0f, 0, 0);
        }
    }
}
