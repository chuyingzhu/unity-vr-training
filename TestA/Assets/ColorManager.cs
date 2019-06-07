using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {
    //public Material[] materials;
    Material m_Material;

    // Start is called before the first frame update
    void Start() {
        m_Material = GetComponent<Renderer>().material;
        //rend.enabled = true;
        //rend.sharedMaterial = materials[0];
    }

    public void changeToGreen() {
        m_Material.color = Color.green;
    }

    public void changeToRed() {
        m_Material.color = Color.red;
    }

    public void changeToBlue() {
        m_Material.color = Color.blue;
    }

    public void changeToBlack() {
        m_Material.color = Color.black;
    }

    void OnDestroy() {
        //Destroy the instance
        Destroy(m_Material);
        //Output the amount of materials to show if the instance was deleted
        //print("Materials " + Resources.FindObjectsOfTypeAll(typeof(Material)).Length);
    }
}