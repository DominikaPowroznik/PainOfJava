using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class QuestionPoint : MonoBehaviour {

    public GameObject questionCanvas;

    private Transform questionTransform;
    private Toggle[] answerToggles = new Toggle[4];

    private Button button;

    private static int index = 0;

    public static List<int> wrongIndexes = new List<int>();

    void OnTriggerEnter2D(Collider2D collider)
    {
        Player player = collider.GetComponent<Player>();

        if (player != null)
        {
            if (index >= QuestionsMaster.questionCount)
            {
                Debug.LogError("Out of questions from json file!");

                //TODO: Place it somewhere else
                QuestionsMaster.arrangeWrongAnswered(wrongIndexes);
                wrongIndexes.Clear();

                Destroy(this.gameObject);

                return;
            }

            //pausing the game
            Time.timeScale = 0.0f;

            questionTransform = questionCanvas.transform.Find("Question");
            questionTransform.GetComponentInChildren<Text>().text = QuestionsMaster.questions[index].question;

            for (int i = 0; i < answerToggles.Length; i++)
            {
                answerToggles[i] = questionCanvas.transform.Find("Answers").Find(i.ToString()).GetComponent<Toggle>();
                answerToggles[i].GetComponentInChildren<Text>().text = QuestionsMaster.questions[index].answers[i].content;
            }

            questionCanvas.SetActive(true);

            button = questionCanvas.transform.Find("Button").GetComponent<Button>();
            button.onClick.AddListener(Check);
        }
    }

    public void Check()
    {
        bool check = true;

        for (int i = 0; i < answerToggles.Length; i++)
        {
            bool answer = QuestionsMaster.questions[index].answers[i].answer.ToLower().Equals("true");
            
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

        if (check)
        {
            Player.PlayerStats.WonPoints++;
        }
        else
        {
            Player.PlayerStats.LostPoints++;
            //Debug.Log("Index zlego:" + index);
            wrongIndexes.Add(index);
            //Debug.Log("Ile zlych:" + wrongIndexes.Count);
        }

        PointsIndicator.SetPoints(Player.PlayerStats.WonPoints, Player.PlayerStats.GetAllPoints());

        button.onClick.RemoveAllListeners();
        button.GetComponentInChildren<Text>().text = "Graj dalej";
        button.onClick.AddListener(GoBack);
    }

    public void GoBack()
    {
        index++;

        //unpausing the game
        Time.timeScale = 1.0f;

        Destroy(this.gameObject);
    }
}
