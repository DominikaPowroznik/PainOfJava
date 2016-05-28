using UnityEngine;
using System.IO;
using LitJson;
using System.Collections.Generic;

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

    public static int questionPointsCount = 0;

    public static int questionsCount;
    public static Question[] questions;

    public static int questionsWithWrongCount;
    public static Question[] questionsWithWrongAnswered;

    void Awake()
    {
        jsonString = File.ReadAllText(Application.dataPath + "/Questions.json");
        itemData = JsonMapper.ToObject(jsonString);

        questionsCount = itemData["questions"].Count;

        questions = new Question[questionsCount];

        for (int i = 0; i < questionsCount; i++)
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

    public static void arrangeWithWrongAnswered(List<int> wrongIndexes)
    {
        questionsWithWrongCount = wrongIndexes.Count + (questionsCount - questionPointsCount);
        questionsWithWrongAnswered = new Question[questionsWithWrongCount];

        for (int i = 0; i < wrongIndexes.Count; i++)
        {
            Debug.Log(questions[wrongIndexes[i]].question);
            questionsWithWrongAnswered[i] = questions[wrongIndexes[i]];
        }

        for(int i = wrongIndexes.Count; i < questionsWithWrongCount; i++)
        {
            Debug.Log(questions[i].question);
            questionsWithWrongAnswered[i] = questions[i];
        }

        RandomizeQuestions(questionsWithWrongAnswered);
    }

    public static void arrangeWrongAnswered(List<int> wrongIndexes)
    {
        for (int i = 0; i < wrongIndexes.Count; i++)
        {
            Debug.Log(questions[wrongIndexes[i]].question);
            questionsWithWrongAnswered[i] = questions[wrongIndexes[i]];
        }
    }

    static void RandomizeQuestions(Question[] array)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)

        //randomize questions in array
        for (int i = 0; i < array.Length; i++)
        {
            Question tmp = array[i];
            int random = Random.Range(i, array.Length);
            array[i] = array[random];
            array[random] = tmp;
        }

        //randomize answers in each question
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < array[i].answers.Length; j++)
            {
                Question.Answers tmp = array[i].answers[j];
                int random = Random.Range(i, array[i].answers.Length);
                array[i].answers[j] = array[i].answers[random];
                array[i].answers[random] = tmp;
            }
        }

        //Debug.Log(array.Length);
        //for (int i = 0; i < array.Length; i++)
        //{
        //    Debug.Log("[" + i + "]->" + array[i].question);
        //    Debug.Log("a)" + array[i].answers[0].content);
        //    Debug.Log("b)" + array[i].answers[1].content);
        //}
    }
}
