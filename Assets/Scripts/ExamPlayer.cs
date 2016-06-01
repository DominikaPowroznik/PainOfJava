using UnityEngine;
using System.Collections;

public class ExamPlayer : MonoBehaviour {

    static int questionsToAnswer;

    private static int _goodAnswered = 0;
    public static int GoodAnswered
    {
        get { return _goodAnswered; }
        set { _goodAnswered = value; }
    }

    private static int _badAnswered = 0;
    public static int BadAnswered
    {
        get { return _badAnswered; }
        set { _badAnswered = value; }
    }

    void Start()
    {
        questionsToAnswer = QuestionsMaster.questionsToBeDisplay.Count;
    }

    
}
