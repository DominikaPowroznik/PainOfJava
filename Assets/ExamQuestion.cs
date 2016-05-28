using UnityEngine;
using UnityEngine.UI;

public class ExamQuestion : QuestionManager {

    bool isFirstRound = true;

    void Start()
    {
        QuestionsMaster.arrangeWithWrongAnswered(wrongIndexes);
        wrongIndexes.Clear();

        FirstRound();
    }

    void FirstRound()
    {
        Debug.Log("First round");
        FillWithData();
        ChangeButton("Odpowiedz", CheckAnswers);
    }

    void NewRound()
    {
        Debug.Log("New round");
        
        if (index >= wrongIndexes.Count)
        {
            index = 0;
            QuestionsMaster.arrangeWrongAnswered(wrongIndexes);
            wrongIndexes.Clear();
        }
        else
        {
            Debug.Log("All questions answered good!");
        }

        if (index < wrongIndexes.Count)
        {
            FillWithData();
            ChangeButton("Odpowiedz", CheckAnswers);
        }
    }

    void CheckAnswers()
    {
        if (IsCorrect())
        {
            Player.PlayerStats.WonPoints++;
        }
        else
        {
            Player.PlayerStats.LostPoints++;
            //Debug.Log("Index zlego:" + index);
            wrongIndexes.Add(index);
            //Debug.Log("Ile zlych:" + wrongIndexes.Count);
        }

        ChangeButton("Dalej", Next);
    }

    void Next()
    {
        UncheckToggles();
        index++;
        if (isFirstRound)
        {
            if (index >= QuestionsMaster.questionsWithWrongCount)
            {
                index = 0;
                QuestionsMaster.arrangeWrongAnswered(wrongIndexes);
                wrongIndexes.Clear();
                isFirstRound = false;
                return;
            }
            FirstRound();
        }
        else
        {
            NewRound();
        }
    }

}
