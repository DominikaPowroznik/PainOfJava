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

    public static int questionPointsCount;
    protected static int spottedQuestionPointsCount;

    protected static List<Question> questionsToBeDisplay = new List<Question>();

    void Awake()
    {
        questionPointsCount = 20;
        spottedQuestionPointsCount = 0;
        questionsToBeDisplay.Clear();

        List<Question> questions = new List<Question>();
        LoadFromJson(questions, "/Questions.json");

        questionsToBeDisplay.AddRange(questions);
        RandomizeQuestions(questionsToBeDisplay);
    }

    void LoadFromJson(List<Question> questions, string path)
    {
        jsonString = File.ReadAllText(Application.dataPath + path);
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
    }

    public static void arrangeWithWrongAnswered(List<int> wrongIndexes, List<int> correctIndexes)
    {
        List<Question> questionsTemp = new List<Question>();
        int questionsWithWrongCount = wrongIndexes.Count + (questionsToBeDisplay.Count - spottedQuestionPointsCount);

        for (int i = 0; i < wrongIndexes.Count; i++)
        {
            questionsTemp.Add(questionsToBeDisplay[wrongIndexes[i]]);
        }

        for (int i = 0; i < questionsToBeDisplay.Count; i++)
        {
            if (!(wrongIndexes.Contains(i) || correctIndexes.Contains(i)))
            {
                questionsTemp.Add(questionsToBeDisplay[i]);
            }
        }

        questionsToBeDisplay.Clear();
        questionsToBeDisplay.AddRange(questionsTemp);

        RandomizeQuestions(questionsToBeDisplay);
    }

    public static void arrangeWrongAnswered(List<int> wrongIndexes)
    {
        List<Question> questionsTemp = new List<Question>();

        for (int i = 0; i < wrongIndexes.Count; i++)
        {
            questionsTemp.Add(questionsToBeDisplay[wrongIndexes[i]]);
        }

        questionsToBeDisplay.Clear();
        questionsToBeDisplay.AddRange(questionsTemp);

        RandomizeQuestions(questionsToBeDisplay);
    }

    static void RandomizeQuestions(List<Question> list)
    {
        // Knuth shuffle algorithm

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
                int random = Random.Range(0, list[i].answers.Length);
                list[i].answers[j] = list[i].answers[random];
                list[i].answers[random] = tmp;
            }
        }
    }
}
