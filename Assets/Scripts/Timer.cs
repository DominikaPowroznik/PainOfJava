using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour{

    Text timeText;
    float time = 30.0f * 60.0f;

    void Awake()
    {
        timeText = transform.GetComponentInChildren<Text>();
    }

    void Update()
    {
        time -= Time.deltaTime;
        timeText.text = (time / 60 - time/60 % 1).ToString() + " : " + (time % 60 - time % 60 % 1).ToString();
    }
}
