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

    public static int questionPointsCount = 20;
    public static int spottedQuestionPointsCount = 0;

    public static List<Question> questions = new List<Question>();
    public static List<Question> questionsWithWrongAnswered = new List<Question>();
    public static List<Question> questionsToBeDisplay = new List<Question>();

    void Awake()
    {
        jsonString = File.ReadAllText(Application.dataPath + "/Questions.json");
        itemData = JsonMapper.ToObject(jsonString);

        int questionsCount = itemData["questions"].Count;

        for (int i = 0; i < questionsCount; i++)
        {
            JsonData jsonItem = itemData["questions"][i];
            Question q = new Question();
            q.question = jsonItem["question"].ToString();
            q.answers = new Question.Answers[4];

            for (int j = 0; j < q.answers.Length; j++)
            {
                q.answers[j].content = jsonItem["answers"][j]["content"].ToString();
                q.answers[j].answer = jsonItem["answers"][j]["answer"].ToString();
            }
            questions.Add(q);
        }

        questionsToBeDisplay = questions;
        //RandomizeQuestions(questionsToBeDisplay);
    }

    public static void arrangeWithWrongAnswered(List<int> wrongIndexes)
    {
        questionsWithWrongAnswered.Clear();
        int questionsWithWrongCount = wrongIndexes.Count + (questions.Count - spottedQuestionPointsCount);

        for (int i = 0; i < wrongIndexes.Count; i++)
        {
            Debug.Log(questions[wrongIndexes[i]].question);
            questionsWithWrongAnswered.Add(questions[wrongIndexes[i]]);
        }

        for(int i = wrongIndexes.Count; i < questionsWithWrongCount; i++)
        {
            Debug.Log(questions[i].question);
            questionsWithWrongAnswered.Add(questions[i]);
        }

        questionsToBeDisplay = questionsWithWrongAnswered;
        //RandomizeQuestions(questionsToBeDisplay);
    }

    public static void arrangeWrongAnswered(List<int> wrongIndexes)
    {
        questionsWithWrongAnswered.Clear();
        for (int i = 0; i < wrongIndexes.Count; i++)
        {
            Debug.Log(questions[wrongIndexes[i]].question);
            questionsWithWrongAnswered.Add(questions[wrongIndexes[i]]);
        }

        questionsToBeDisplay = questionsWithWrongAnswered;
        //RandomizeQuestions(questionsToBeDisplay);
    }

    static void RandomizeQuestions(List<Question> list)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)

        //randomize questions in array
        for (int i = 0; i < list.Count; i++)
        {
            Question tmp = list[i];
            int random = Random.Range(i, list.Count);
            list[i] = list[random];
            list[random] = tmp;
        }

        //randomize answers in each question
        for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < list[i].answers.Length; j++)
            {
                Question.Answers tmp = list[i].answers[j];
                int random = Random.Range(i, list[i].answers.Length);
                list[i].answers[j] = list[i].answers[random];
                list[i].answers[random] = tmp;
            }
        }

        //Debug.Log(list.Count);
        //for (int i = 0; i < list.Count; i++)
        //{
        //    Debug.Log("[" + i + "]->" + array[i].question);
        //    Debug.Log("a)" + array[i].answers[0].content);
        //    Debug.Log("b)" + array[i].answers[1].content);
        //}
    }
}
