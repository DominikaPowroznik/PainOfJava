using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PointsCounter : MonoBehaviour {

    private Text pointsText;

	void Awake ()
    {
        pointsText = this.transform.GetComponentInChildren<Text>();
	}
	
	void Update ()
    {
        pointsText.text = Player.PlayerStats.WonPoints + " / " + QuestionsMaster.questionCount;
    }
}
