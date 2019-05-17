using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMan : MonoBehaviour {
    // Placeholder captions
    string[] steps = {"zero", "one", "two", "three"};
    int index = 0;
    public Text msg;

    public void NextStep() {
        msg.text = steps[index++];
        if (index == steps.Length) {
            index = 0;
        }
    }

    public void EndGame() {
        Debug.Log("Game Over");
    }
}
