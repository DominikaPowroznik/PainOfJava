using UnityEngine;
using UnityEngine.UI;

public class Mark : MonoBehaviour {

    private Text markText;
    private int mark = 2;

    void Awake()
    {
        markText = transform.GetComponentInChildren<Text>();
        markText.color = new Color(1.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        float quotient = (float)Player.PlayerStats.WonPoints/(float)QuestionsMaster.questionCount;
        
        if (quotient > 0.9)
        {
            mark = 5;
            markText.color = new Color(0.0f, 1.0f, 0.0f);
        }
        else if(quotient > 0.75)
        {
            mark = 4;
            markText.color = new Color(0.0f, 1.0f, 1.0f);
        }
        else if(quotient > 0.5)
        {
            mark = 3;
            markText.color = new Color(1.0f, 1.0f, 0.0f);
        }
        markText.text = mark.ToString();
    }
}
