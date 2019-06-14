using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidManager : MonoBehaviour {
    MeshRenderer meshRenderer;
    float fillAmount;

    // Start is called before the first frame update
    void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
        // 0.5 = empty, 0.4 = half full, 0.2 = almost full
        meshRenderer.material.SetFloat("_FillAmount", 0.5f);
        fillAmount = meshRenderer.material.GetFloat("_FillAmount");
    }

    public void FillContainer() {
        fillAmount -= 0.01f;
        meshRenderer.material.SetFloat("_FillAmount", fillAmount);
    }
}
