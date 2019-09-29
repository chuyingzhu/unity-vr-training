using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonTrigger : MonoBehaviour
{
    // Floating Text
    public GameObject TextPrefab;
    public GameObject parent;

    // Event for when triggered
    public UnityEvent TriggerHit;
    private bool checker = false;

    // Detecting if a GameObj with trigger has collided
    void OnTriggerEnter(Collider obj)
    {
        TriggerHit.Invoke();
    }

    // Make a desired event, HAS TO BE PUBLIC
    public void PrinttoCanvas(string text)
    {
        Debug.Log("Hit");
        if (checker == false)
        {
            var textMesh = Instantiate(TextPrefab, new Vector3(-0.03f, 1.53f, -6.108f), Quaternion.identity, parent.transform);
            textMesh.GetComponent<TextMesh>().text = "ON";
            Destroy(textMesh, 1.5f);
            checker = true;
            Debug.Log("True");
        }
        else if (checker == true)
        {
            var textMesh = Instantiate(TextPrefab, new Vector3(-0.03f, 1.53f, -6.108f), Quaternion.identity, parent.transform);
            textMesh.GetComponent<TextMesh>().text = "OFF";
            Destroy(textMesh, 1.5f);
            checker = false;
            Debug.Log("False");
        }
    } 
}
