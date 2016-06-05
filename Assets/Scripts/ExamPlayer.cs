using UnityEngine;
using System.Collections;

public class ExamPlayer : MonoBehaviour {

    private static int _goodAnswered;
    public static int GoodAnswered
    {
        get { return _goodAnswered; }
        set { _goodAnswered = value; }
    }

    private static int _badAnswered;
    public static int BadAnswered
    {
        get { return _badAnswered; }
        set { _badAnswered = value; }
    }

    public static int GetAllAttempts()
    {
        return BadAnswered + GoodAnswered;
    }

    [SerializeField]
    private GameObject pauseUI;

    void OnLevelWasLoaded(int level)
    {
        GoodAnswered = 0;
        BadAnswered = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //pausing the game
            Time.timeScale = 0.0f;

            pauseUI.SetActive(true);
        }
    }
}
