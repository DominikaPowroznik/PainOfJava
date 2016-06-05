using UnityEngine;
using UnityEngine.UI;

public class ExamQuestion : QuestionManager {

    public GameObject summaryUI;

    void Start()
    {
        QuestionsMaster.arrangeWithWrongAnswered(wrongIndexes, correctIndexes);
        wrongIndexes.Clear();

        NewRound();
    }

    void NewRound()
    {
        FillWithData();
        ChangeButton("Odpowiedz", CheckAnswers);
    }

    void CheckAnswers()
    {
        Debug.Log(index);
        if (IsCorrect())
        {
            ExamPlayer.GoodAnswered++;
            correctIndexes.Add(index);
        }
        else
        {
            ExamPlayer.BadAnswered++;
            wrongIndexes.Add(index);
        }

        ChangeButton("Dalej", Next);
    }

    void Next()
    {
        UncheckToggles();
        index++;
        if (index >= QuestionsMaster.questionsToBeDisplay.Count)
        {
            index = 0;
            if (wrongIndexes.Count > 0)
            {
                QuestionsMaster.arrangeWrongAnswered(wrongIndexes);
                wrongIndexes.Clear();
            }
            else
            {
                summaryUI.SetActive(true);
            }
        }
        NewRound();
    }

}
