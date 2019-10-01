using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(Rigidbody))]
public class Interactable : MonoBehaviour {
    public Vector3 m_Offset = Vector3.zero;
    //
    [HideInInspector]
    public Hand m_ActiveHand = null;
    public bool isTrolley = false;
    private Transform tHand;
    Quaternion newRotation;
    // Virtual to fit different needs (eg. gun vs grenade)
    public virtual void Action() {
        if (gameObject.CompareTag("Flask")) {
            transform.GetChild(0).gameObject.GetComponent<LiquidManager>().FillContainer();
        }
    }
    private void Update()
    {
        if (isTrolley)
        {
            newRotation = Quaternion.Euler(0, tHand.transform.eulerAngles.y, 0);
            this.transform.rotation = newRotation;
            transform.position = new Vector3(tHand.transform.position.x, transform.position.y, tHand.transform.position.z);
        }
    }
    public void ApplyOffset(Transform hand) {
        transform.SetParent(hand);
        transform.localRotation = Quaternion.identity;
        transform.localPosition = m_Offset;
        transform.SetParent(null);
    }
    public void ApplyOffsetT(Transform hand)
    {
        tHand = hand;
        newRotation = Quaternion.Euler(0, hand.transform.eulerAngles.y, 0);
        this.transform.rotation = newRotation;
        transform.position = new Vector3(hand.transform.position.x,transform.position.y, hand.transform.position.z);
        Debug.Log("trolley " + this.gameObject.name + " activated");
    }
}