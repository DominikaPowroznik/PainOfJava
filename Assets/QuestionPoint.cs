using UnityEngine;
using UnityEngine.UI;

public class QuestionPoint : QuestionsMaster {

    public GameObject questionCanvas;

    private Transform questionTransform;
    private Toggle[] answerToggles = new Toggle[4];

    private static int index = 0;

    void OnTriggerEnter2D(Collider2D collider)
    {
        Player player = collider.GetComponent<Player>();

        if (player != null)
        {
            if (index >= questionCount)
            {
                index = 0;
            }

            //pausing the game
            Time.timeScale = 0.0f;

            questionTransform = questionCanvas.transform.Find("Question");
            questionTransform.GetComponentInChildren<Text>().text = questions[index].question;

            for (int i = 0; i < answerToggles.Length; i++)
            {
                answerToggles[i] = questionCanvas.transform.Find("Answers").Find(i.ToString()).GetComponent<Toggle>();
                answerToggles[i].GetComponentInChildren<Text>().text = questions[index].answers[i].content;
            }

            questionCanvas.SetActive(true);
        }
    }

    public void Check()
    {
        for (int i = 0; i < answerToggles.Length; i++)
        {
            bool a = questions[index].answers[i].answer.ToLower().Equals("true");

            if (a && answerToggles[i].isOn)
            {
                answerToggles[i].GetComponentInChildren<Text>().color = new Color(0.0f, 0.5f, 0.0f, 1.0f);
                answerToggles[i].GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;
            }
            else if (a && !answerToggles[i].isOn)
            {
                answerToggles[i].GetComponentInChildren<Text>().color = new Color(0.0f, 0.5f, 0.0f, 1.0f);
            }
            else if (!a && answerToggles[i].isOn)
            {
                answerToggles[i].GetComponentInChildren<Text>().color = new Color(0.8f, 0.0f, 0.0f, 1.0f);
            }

            answerToggles[i].interactable = false;
        }

        Button button = questionCanvas.transform.Find("Button").GetComponent<Button>();
        button.GetComponentInChildren<Text>().text = "Graj dalej";
        button.onClick.AddListener(() => GoBack());
    }

    public void GoBack()
    {
        index++;

        //unpausing the game
        Time.timeScale = 1.0f;

        Destroy(this.gameObject);
    }
}
