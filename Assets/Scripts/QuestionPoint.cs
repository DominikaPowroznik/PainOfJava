using UnityEngine;
using UnityEngine.UI;

public class QuestionPoint : QuestionManager {

    void OnTriggerEnter2D(Collider2D collider)
    {
        Player player = collider.GetComponent<Player>();

        if (player != null)
        {
            if (index >= QuestionsMaster.questionsToBeDisplay.Count)
            {
                Debug.LogError("Out of questions!");

                //TODO: Place it somewhere else
                QuestionsMaster.arrangeWithWrongAnswered(wrongIndexes);
                wrongIndexes.Clear();

                Destroy(this.gameObject);

                return;
            }

            //pausing the game
            Time.timeScale = 0.0f;

            QuestionsMaster.questionPointsCount++;

            FillWithData();
            questionCanvas.SetActive(true);
            ChangeButton("Odpowiedz", CheckAnswers);
        }
    }

    public void CheckAnswers()
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

        PointsIndicator.SetPoints(Player.PlayerStats.WonPoints, Player.PlayerStats.GetAllPoints());

        ChangeButton("Graj dalej", GoBack);
    }

    public void GoBack()
    {
        index++;

        //unpausing the game
        Time.timeScale = 1.0f;

        Destroy(this.gameObject);
    }
}
