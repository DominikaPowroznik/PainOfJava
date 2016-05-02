using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class QuestionPoint : MonoBehaviour {

    public GameObject questionCanvas;

    private Transform question;
    private Toggle[] answerToggles = new Toggle[4];

    private string jsonString;
    private JsonData itemData;
    private JsonData q;

    private int questionCount;
    private int[] questionNumber;
    private static int index = 0;

    void Start()
    {
        jsonString = File.ReadAllText(Application.dataPath + "/Questions.json");
        itemData = JsonMapper.ToObject(jsonString);

        questionCount = itemData["questions"].Count;

        questionNumber = new int[questionCount];
        for (int i = 0; i < questionCount; i++)
        {
            questionNumber[i] = i;
        }
        RandomizeArray(questionNumber);

        Debug.Log("Question number: " + questionNumber[0]);
        //Debug.Log(itemData["questions"][1]["question"]);
    }

    void RandomizeArray(int[] array)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int i = 0; i < array.Length; i++)
        {
            int tmp = array[i];
            int random = Random.Range(i, array.Length);
            array[i] = array[random];
            array[random] = tmp;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //pausing the game
        Time.timeScale = 0.0f;

        Debug.Log("Index: " + index);

        question = questionCanvas.transform.Find("Question");
        q = itemData["questions"][questionNumber[index]];
        index++;
        question.GetComponentInChildren<Text>().text = q["question"].ToString();

        for (int i  = 0; i < answerToggles.Length; i++)
        {
            answerToggles[i] = questionCanvas.transform.Find("Answers").Find(i.ToString()).GetComponent<Toggle>();
            answerToggles[i].GetComponentInChildren<Text>().text = q["answers"][i]["content"].ToString();
        }

        questionCanvas.SetActive(true);
    }

    public void Check()
    {
        for (int i = 0; i < answerToggles.Length; i++)
        {
            JsonData a = q["answers"][i]["answer"];

            if (a.Equals(true))
            {
                answerToggles[i].GetComponentInChildren<Text>().color = Color.green;
            }
            else
            {
                answerToggles[i].GetComponentInChildren<Text>().color = Color.red;
            }

            //if (answerToggles[i].isOn)
            //{
            //}
            //else
            //{
            //}

            answerToggles[i].interactable = false;
        }

        Button button = questionCanvas.transform.Find("Button").GetComponent<Button>();
        button.GetComponentInChildren<Text>().text = "Graj dalej";
        button.onClick.AddListener(() => GoBack());
    }

    public void GoBack()
    {
        //unpausing the game
        Time.timeScale = 1.0f;

        Destroy(this.gameObject);
    }
}
