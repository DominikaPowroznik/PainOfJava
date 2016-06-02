using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SummaryUI1 : MonoBehaviour {

    Text answersCountText;
    Text percentOfCorrectCountText;
    Text markCountText;

    void Awake()
    {
        answersCountText = transform.Find("AnswersCount").GetComponent<Text>();
        percentOfCorrectCountText = transform.Find("PercentOfCorrectCount").GetComponent<Text>();
        markCountText = transform.Find("MarkCount").GetComponent<Text>();
    }

    void OnEnable()
    {
        //pausing the game
        Time.timeScale = 0.0f;

        answersCountText.text = Player.PlayerStats.GetAllPoints().ToString() + " / " + QuestionsMaster.questionPointsCount.ToString();

        if (Player.PlayerStats.GetAllPoints() > 0)
        {
            percentOfCorrectCountText.text = ((100 * Player.PlayerStats.WonPoints) / Player.PlayerStats.GetAllPoints()).ToString() + " %";
        }
        else
        {
            percentOfCorrectCountText.text = "0 %";
        }

        int mark;
        float quotient = (float)Player.PlayerStats.WonPoints / (float)QuestionsMaster.questionPointsCount;
        if (quotient > 0.9)
        {
            mark = 5;
            markCountText.color = new Color(0.0f, 1.0f, 0.0f);
        }
        else if (quotient > 0.75)
        {
            mark = 4;
            markCountText.color = new Color(0.0f, 1.0f, 1.0f);
        }
        else if (quotient > 0.5)
        {
            mark = 3;
            markCountText.color = new Color(1.0f, 1.0f, 0.0f);
        }
        else
        {
            mark = 2;
            markCountText.color = new Color(1.0f, 0.0f, 0.0f);
        }
        markCountText.text = mark.ToString();
    }

    public void LoadNextLevel()
    {
        //unpausing the game
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
