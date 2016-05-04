using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

public class QuestionsMaster : MonoBehaviour {

    public struct Question
    {
        public string question;
        public struct Answers
        {
            public string content;
            public string answer; 
        }
        public Answers[] answers;
    }

    private string jsonString;
    private JsonData itemData;

    public static int questionCount;
    public static Question[] questions;

    void Start()
    {
        jsonString = File.ReadAllText(Application.dataPath + "/Questions.json");
        itemData = JsonMapper.ToObject(jsonString);

        questionCount = itemData["questions"].Count;

        questions = new Question[questionCount];

        for (int i = 0; i < questionCount; i++)
        {
            JsonData q = itemData["questions"][i];
            questions[i].question = q["question"].ToString();

            questions[i].answers = new Question.Answers[4];

            for (int j = 0; j < questions[i].answers.Length; j++)
            {
                questions[i].answers[j].content = q["answers"][j]["content"].ToString();
                questions[i].answers[j].answer = q["answers"][j]["answer"].ToString();
            }
        }
        
        RandomizeQuestions(questions);
    }

    void RandomizeQuestions(Question[] array)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int i = 0; i < array.Length; i++)
        {
            Question tmp = array[i];
            int random = Random.Range(i, array.Length);
            array[i] = array[random];
            array[random] = tmp;
        }
        Debug.Log(array.Length);
        for (int i = 0; i < array.Length; i++)
        {
            Debug.Log("[" + i + "]->" + array[i].question);
        }
    }

}
