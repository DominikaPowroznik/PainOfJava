using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestionManager : QuestionsMaster
{
    [SerializeField]
    protected GameObject questionCanvas;

    private Transform questionTransform;
    private Toggle[] answerToggles = new Toggle[4];
    private Button button;

    protected static int index;
    protected static List<int> wrongIndexes = new List<int>();
    protected static List<int> correctIndexes = new List<int>();

    void Awake()
    {
        button = questionCanvas.transform.Find("Button").GetComponent<Button>();
        questionTransform = questionCanvas.transform.Find("Question");      
    }

    void OnLevelWasLoaded(int level)
    {
        index = 0;
    }

    protected void FillWithData()
    {
        questionTransform.GetComponentInChildren<Text>().text = QuestionsMaster.questionsToBeDisplay[index].question;

        for (int i = 0; i < answerToggles.Length; i++)
        {
            answerToggles[i] = questionCanvas.transform.Find("Answers").Find(i.ToString()).GetComponent<Toggle>();
            answerToggles[i].GetComponentInChildren<Text>().text = QuestionsMaster.questionsToBeDisplay[index].answers[i].content;
        }
    }

    protected bool IsCorrect()
    {
        bool check = true;

        for (int i = 0; i < answerToggles.Length; i++)
        {
            bool answer = QuestionsMaster.questionsToBeDisplay[index].answers[i].answer.ToLower().Equals("true");

            if (answer && answerToggles[i].isOn)
            {
                answerToggles[i].GetComponentInChildren<Text>().color = new Color(0.0f, 0.5f, 0.0f, 1.0f);
                answerToggles[i].GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;
            }
            else if (answer && !answerToggles[i].isOn)
            {
                answerToggles[i].GetComponentInChildren<Text>().color = new Color(0.0f, 0.5f, 0.0f, 1.0f);
                check = false;
            }
            else if (!answer && answerToggles[i].isOn)
            {
                answerToggles[i].GetComponentInChildren<Text>().color = new Color(0.8f, 0.0f, 0.0f, 1.0f);
                check = false;
            }

            answerToggles[i].interactable = false;
        }

        return check;
    }

    protected void UncheckToggles()
    {
        for (int i = 0; i < answerToggles.Length; i++)
        {
            answerToggles[i].GetComponentInChildren<Text>().color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            answerToggles[i].isOn = false;
            answerToggles[i].interactable = true;
        }
    }

    public void ChangeButton(string text, UnityEngine.Events.UnityAction listener)
    {
        button.onClick.RemoveAllListeners();
        button.GetComponentInChildren<Text>().text = text;
        button.onClick.AddListener(listener);
    }

}
