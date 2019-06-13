using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidManager : MonoBehaviour {
    Material m_Material;

    // Start is called before the first frame update
    void Start() {
        m_Material = GetComponent<Renderer>().material;
        m_Material.shader._FillAmount = 0;
    }

    // Update is called once per frame
    void Update() {
        
    }
}
