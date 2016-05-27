using UnityEngine;

public class PauseUI : MonoBehaviour {

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //unpausing the game
            Time.timeScale = 1.0f;

            this.gameObject.SetActive(false);
        }
    }

    public void Quit()
    {
        Debug.Log("Game quit");
        Application.Quit();
    }

    public void Reload()
    {
        //unpausing the game
        Time.timeScale = 1.0f;

        this.gameObject.SetActive(false);
    }
}
