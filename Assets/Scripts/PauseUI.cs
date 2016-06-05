using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Reload()
    {
        //unpausing the game
        Time.timeScale = 1.0f;

        this.gameObject.SetActive(false);
    }
}
