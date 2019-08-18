using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIFollow : MonoBehaviour {
	public TextMeshPro text;
	public Vector3 offset;

    // Update is called once per frame
    void Update() {
        text.transform.position = this.transform.position + offset;
    }
}
