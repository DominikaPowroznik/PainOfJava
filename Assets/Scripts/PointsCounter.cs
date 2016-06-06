using UnityEngine;
using UnityEngine.UI;

public class PointsCounter : MonoBehaviour {

    private Text pointsText;

	void Awake ()
    {
        pointsText = this.transform.GetComponentInChildren<Text>();
	}
	
	void Update ()
    {
        pointsText.text = Player.PlayerStats.WonPoints + " / " + Player.PlayerStats.GetAllPoints();
    }
}
