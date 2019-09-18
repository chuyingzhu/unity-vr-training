using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable : MonoBehaviour
{
    [SerializeField]
    private TextMesh Tm;
    [SerializeField]
    private Light l;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float r = transform.localRotation.eulerAngles.y;
        float value;

        value = 360 - r;

        // In case player turns too little
        if (r < 0.5f) value = 0;

        // In case player turns too much
        if (value > 270) value = 270;

        value = value / 270 * 100;
        Tm.text = value.ToString("0.00");
        value /= 100;
        l.intensity = value;
    }
}
