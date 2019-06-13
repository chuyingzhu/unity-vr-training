using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidManager : MonoBehaviour {
    MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.SetFloat("_FillAmount", 0.2f);
    }

    // Update is called once per frame
    void Update() {
        
    }
}
