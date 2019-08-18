using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFollow : MonoBehaviour {
	public Text label;
	public Camera m_Camera;

    // Update is called once per frame
    void Update() {
        Vector3 namePos = m_Camera.WorldToScreenPoint(this.transform.position);
        label.transform.position = namePos;
    }
}
