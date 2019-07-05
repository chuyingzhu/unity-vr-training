using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MCManager : MonoBehaviour {
    public Button continueButton;
    private Text buttonText;

    private void Awake() {
        continueButton.enabled = false;
        buttonText = continueButton.transform.GetChild(0).gameObject.GetComponent<Text>();
    }

    public void nextQuestion() {
        buttonText.text = "Next Question";
        continueButton.enabled = true;
    }

    public void tryAgain() {
        buttonText.text = "Try Again";
        continueButton.enabled = true;
    }
}
