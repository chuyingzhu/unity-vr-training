using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MCManager : MonoBehaviour {
    public Button continueButton;
    public List<Button> MCButtons;
    public Text questionText;
    public Text status;

    private Text buttonText;
    private string[] questions = new string[3] {"1. What is 1+1?", "2. What is 9*8?", "3. What is 12^2?"};
    private string[,] choices = new string[3,4] {{"1","2","3","4"}, {"no","yes","72","0"}, {"144","idk","infinity","undefined"}};
    private int[] answers = new int[3] {1, 2, 0};
    private int questionNumber = 0;

    private void Awake() {
        //continueButton.GetComponent<Renderer>().enabled = false;
        continueButton.enabled = false;
        buttonText = continueButton.transform.GetChild(0).gameObject.GetComponent<Text>();
        nextQuestion();
    }

    // When user picks the right answer, display status
    public void rightAnswer() {
        status.text = "Correct!";
        buttonText.text = "Continue";
        //continueButton.GetComponent<Renderer>().enabled = true;
        continueButton.enabled = true;
        disableMCButtons();
    }

    // When user picks the wrong answer, display status
    public void wrongAnswer() {
        status.text = "Incorrect!";
        buttonText.text = "Continue";
        //continueButton.GetComponent<Renderer>().enabled = true;
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

    // Change the question, choices, and answers
    public void nextQuestion() {
        if (questionNumber >= questions.Length) {
            return;
        }
        // Assign question
        questionText.text = questions[questionNumber];
        // Assign choices
        for (int i=0; i<4; i++) {
            MCButtons[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = choices[questionNumber, i];
            // Assign which choices are correct/wrong
            bool isCorrect = (i==answers[questionNumber]);
            MCButtons[i].GetComponent<ButtonTransitioner>().isCorrect = isCorrect;
            MCButtons[i].GetComponent<ButtonTransitioner>().m_DownColor = isCorrect ? Color.green : Color.red;
        }
        questionNumber++;
        status.text = "";
    }
}
