using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

    //we want to have only 1 instance of GM
    public static GameMaster gm;

    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 2f;

    private static int remainingLives = 3;
    public static int RemainingLives
    {
        get { return remainingLives;  }
    }

    [SerializeField]
    private GameObject gameOverUI;

    void Start()
    {
        if(gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>(); ;
        }
    }

    public void EndGame()
    {
        Debug.Log("END");
        gameOverUI.SetActive(true); 
    }

    public IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(spawnDelay);
        if(GameObject.FindGameObjectWithTag("Player") == null)
        {
            Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        }   
    }

	public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);

        GameObject.Find("LivesHeart " + remainingLives).SetActive(false);
        remainingLives--;

        if(remainingLives <= 0)
        {
            gm.EndGame();
        }
        else
        {
            gm.StartCoroutine(gm.RespawnPlayer());
        }
    }

    public static void KillEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}
