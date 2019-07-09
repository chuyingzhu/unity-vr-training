using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MCManager : MonoBehaviour {
    public Button continueButton;
    public List<Button> MCButtons;
    public Text questionText;
    public Text status;
    public CanvasGroup continueCanvasGroup;

    private Text buttonText;
    private string[] questions = new string[3] {"1. What is 1+1?", "2. What is 9*8?", "3. What is 12^2?"};
    private string[,] choices = new string[3,4] {{"1","2","3","4"}, {"no","yes","72","0"}, {"144","idk","infinity","undefined"}};
    private int[] answers = new int[3] {1, 2, 0};
    private int questionNumber = 0;

    private void Awake() {
        buttonText = continueButton.transform.GetChild(0).gameObject.GetComponent<Text>();
        nextQuestion();
    }

    // When user picks the right answer, display status
    public void rightAnswer() {
        status.text = "Correct!";
        buttonText.text = "Continue";
        Show(continueCanvasGroup);
        disableMCButtons();
    }

    // When user picks the wrong answer, display status
    public void wrongAnswer() {
        status.text = "Incorrect!";
        buttonText.text = "Continue";
        Show(continueCanvasGroup);
        disableMCButtons();
    }

    private void disableMCButtons() {
        for (int i=0; i<4; i++) {
            bool isCorrect = (i==answers[questionNumber-1]);
            MCButtons[i].GetComponent<ButtonTransitioner>().m_NormalColor = isCorrect ? Color.green : Color.red;
            MCButtons[i].GetComponent<ButtonTransitioner>().m_HoverColor = isCorrect ? Color.green : Color.red;
            MCButtons[i].GetComponent<ButtonTransitioner>().m_DownColor = isCorrect ? Color.green : Color.red;
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
            MCButtons[i].GetComponent<ButtonTransitioner>().m_NormalColor = Color.white;
            MCButtons[i].GetComponent<ButtonTransitioner>().m_HoverColor = Color.grey;
            MCButtons[i].GetComponent<ButtonTransitioner>().m_DownColor = isCorrect ? Color.green : Color.red;
        }
        status.text = "";
        Hide(continueCanvasGroup);
        questionNumber++;
    }

    private void Hide(CanvasGroup m_CanvasGroup) {
        m_CanvasGroup.alpha = 0f; //this makes everything transparent
        m_CanvasGroup.blocksRaycasts = false; //this prevents the UI element to receive input events
    }

    private void Show(CanvasGroup m_CanvasGroup) {
        m_CanvasGroup.alpha = 1f;
        m_CanvasGroup.blocksRaycasts = true;
    }
}