using UnityEngine;
using System.Collections;

public class QuestionPoint : MonoBehaviour {

    public GameObject questionCanvas;

	void OnTriggerEnter2D(Collider2D col)
    {
        questionCanvas.SetActive(true);
    }

    public void Answer()
    {
        questionCanvas.SetActive(false);
    }
}
