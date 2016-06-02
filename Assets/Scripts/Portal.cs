using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

    public GameObject summaryUI;

    void OnTriggerEnter2D(Collider2D collider)
    {
        Player player = collider.GetComponent<Player>();

        if (player != null)
        {
            summaryUI.SetActive(true);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
