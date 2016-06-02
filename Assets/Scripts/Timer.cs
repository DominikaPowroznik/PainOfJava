using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour{

    Text timerText;
    public static float timeForTest = 30.0f * 60.0f;
    public static float timeLeft;

    public GameObject summaryUI;

    void Awake()
    {
        timeLeft = timeForTest;
        timerText = transform.GetComponentInChildren<Text>();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        timerText.text = (timeLeft / 60 - timeLeft / 60 % 1).ToString() + " : " + (timeLeft % 60 - timeLeft % 60 % 1).ToString();

        if(timeLeft <= 0)
        {
            summaryUI.SetActive(true); ;
        }
    }
}
