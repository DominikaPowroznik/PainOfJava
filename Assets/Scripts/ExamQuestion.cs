using UnityEngine;

public class ExamQuestion : QuestionManager {

    [SerializeField]
    private GameObject summaryUI;

    void Start()
    {
        arrangeWithWrongAnswered(wrongIndexes, correctIndexes);
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
        if (index >= questionsToBeDisplay.Count)
        {
            index = 0;
            if (wrongIndexes.Count > 0)
            {
                arrangeWrongAnswered(wrongIndexes);
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
