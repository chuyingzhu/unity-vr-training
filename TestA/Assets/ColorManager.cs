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

    public void changeColor() {
        m_Material.color = Color.green;
    }

    void Update() {
        m_Material.color = Color.green;
    }
}