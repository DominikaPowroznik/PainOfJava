using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SummaryUI2 : MonoBehaviour {

    private Text badAnswersCountText;
    private Text percentOfCorrectCountText;
    private Text timeCountText;
    private Text markCountText;

    void Awake()
    {
        badAnswersCountText = transform.Find("BadAnswersCount").GetComponent<Text>();
        percentOfCorrectCountText = transform.Find("PercentOfCorrectCount").GetComponent<Text>();
        timeCountText = transform.Find("TimeCount").GetComponent<Text>();
        markCountText = transform.Find("MarkCount").GetComponent<Text>();
    }

    void OnEnable()
    {
        //pausing the game
        Time.timeScale = 0.0f;

        badAnswersCountText.text = ExamPlayer.BadAnswered.ToString() + " / " + ExamPlayer.GetAllAttempts().ToString();

        if (ExamPlayer.GetAllAttempts() > 0)
        {
            percentOfCorrectCountText.text = ((100 * ExamPlayer.GoodAnswered) / ExamPlayer.GetAllAttempts()).ToString() + " %";
        }
        else
        {
            percentOfCorrectCountText.text = "0 %";
        }

        float time = Timer.timeForTest - Timer.timeLeft;
        timeCountText.text = (time / 60 - time / 60 % 1).ToString() + "min " + (time % 60 - time % 60 % 1).ToString() + "s";

        int mark;
        float quotient = (float)ExamPlayer.GoodAnswered / (float)ExamPlayer.GetAllAttempts() + (Timer.timeLeft / Timer.timeForTest) / 2;
        if (quotient > 1.2)
        {
            mark = 5;
            markCountText.color = new Color(0.0f, 1.0f, 0.0f);
        }
        else if (quotient > 0.8)
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

    public void LoadMenu()
    {
        //unpausing the game
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
