using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MCManager : MonoBehaviour {
    public Button continueButton;
    public List<Button> MCButtons;
    private Text buttonText;

    private void Awake() {
        continueButton.GetComponent<Renderer>().enabled = false;
        continueButton.enabled = false;
        buttonText = continueButton.transform.GetChild(0).gameObject.GetComponent<Text>();
    }

    public void nextQuestion() {
        buttonText.text = "Next Question";
        continueButton.GetComponent<Renderer>().enabled = true;
        continueButton.enabled = true;
        disableMCButtons();
    }

    public void tryAgain() {
        buttonText.text = "Try Again";
        continueButton.GetComponent<Renderer>().enabled = true;
        continueButton.enabled = true;
        disableMCButtons();
    }

    private void disableMCButtons() {
        foreach (Button b in MCButtons) {
            b.enabled = false;
        }
    }

    public void enableMCButtons() {
        foreach (Button b in MCButtons) {
            b.enabled = true;
        }
    }
}
