using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuestionPoint : MonoBehaviour {

    public GameObject questionCanvas;

	void OnTriggerEnter2D(Collider2D col)
    {
        //pausing the game
        Time.timeScale = 0.0f;

        questionCanvas.SetActive(true);
    }

    public void Check()
    {
        Transform question = questionCanvas.transform.Find("Question");
        Toggle a = question.Find("A").GetComponent<Toggle>();
        Toggle b = question.Find("B").GetComponent<Toggle>();
        Toggle c = question.Find("C").GetComponent<Toggle>();
        Toggle d = question.Find("D").GetComponent<Toggle>();

        if (a.isOn)
        {
            a.GetComponentInChildren<Text>().color = Color.red;
        }

        if (b.isOn)
        {
            b.GetComponentInChildren<Text>().color = Color.green;
        }

        if (c.isOn)
        {
            c.GetComponentInChildren<Text>().color = Color.red;
        }

        if (d.isOn)
        {
            d.GetComponentInChildren<Text>().color = Color.red;
        }

        a.interactable = false;
        b.interactable = false;
        c.interactable = false;
        d.interactable = false;

        Button button = questionCanvas.transform.Find("Button").GetComponent<Button>();
        button.GetComponentInChildren<Text>().text = "Graj dalej";
        button.onClick.AddListener(() => GoBack());
    }

    public void GoBack()
    {
        //unpausing the game
        Time.timeScale = 1.0f;

        Destroy(this.gameObject);
    }
}
