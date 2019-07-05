using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTransitioner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler {
    public Color32 m_NormalColor = Color.white;
    public Color32 m_HoverColor = Color.grey;
    public Color32 m_DownColor = Color.white;
    public Text buttonText;
    public Text question;
    public Text status;
    public bool isCorrect;
    public GameObject m_MCManager;

    private Image m_Image = null;

    private void Awake() {
        m_Image = GetComponent<Image>();
        if (isCorrect) {
            m_DownColor = Color.green;
        }
        else {
            m_DownColor = Color.red;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        //print("Enter");
        m_Image.color = m_HoverColor;
    }

    public void OnPointerExit(PointerEventData eventData) {
        //print("Exit");
        m_Image.color = m_NormalColor;
    }

    public void OnPointerDown(PointerEventData eventData) {
        print("Down");
        m_Image.color = m_DownColor;
    }

    public void OnPointerUp(PointerEventData eventData) {
        print("Up");
    }

    public void OnPointerClick(PointerEventData eventData) {
        print("Click");
        m_Image.color = m_HoverColor;
        if (isCorrect) {
            status.text = "Correct!";
            m_MCManager.GetComponent<MCManager>().nextQuestion();
        }
        else {
            status.text = "Incorrect.";
            m_MCManager.GetComponent<MCManager>().tryAgain();
        }
    }
}