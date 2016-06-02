using UnityEngine;
using UnityEngine.UI;

public class ExamQuestion : QuestionManager {

    int wrongCount;
    public GameObject summaryUI;

    void Start()
    {
        QuestionsMaster.arrangeWithWrongAnswered(wrongIndexes);
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
        }
        else
        {
            ExamPlayer.BadAnswered++;
            Debug.Log("Index zlego:" + index);
            wrongIndexes.Add(index);
            Debug.Log("Ile zlych:" + wrongIndexes.Count);
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
            wrongCount = wrongIndexes.Count;
            if (wrongCount > 0)
            {
                QuestionsMaster.arrangeWrongAnswered(wrongIndexes);
                wrongIndexes.Clear();
            }
            else
            {
                summaryUI.SetActive(true);
                //Debug.Log("Koniec gry - wygrałeś?!");
            }
        }
        NewRound();
    }

}
