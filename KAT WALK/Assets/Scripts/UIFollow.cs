using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIFollow : MonoBehaviour {
	public TextMeshPro text;
	public float offset;
	public Camera m_Camera;
    public float cRotation;
    // Update is called once per frame
    void Update()
    {
        cRotation = m_Camera.transform.rotation.y;
        //text.transform.position = this.transform.position + offset;
        text.transform.position = new Vector3(m_Camera.transform.position.x+offset*Mathf.Sin(cRotation), m_Camera.transform.position.y, m_Camera.transform.position.z+offset*Mathf.Cos(cRotation));
        text.transform.rotation = Quaternion.Euler(0.0f, m_Camera.transform.rotation.y, 0.0f);
        // Flip the text if head turns around (90 to 270 degrees)
       /*     if (m_Camera.transform.localEulerAngles.y > 90 && m_Camera.transform.localEulerAngles.y < 270) {
                text.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }
            else{
                text.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            */
        }
        

    
}