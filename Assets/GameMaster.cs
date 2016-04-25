using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

    //we want to have only 1 instance of GM
    public static GameMaster gm;

    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 2f;

    void Start()
    {
        if(gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>(); ;
        }
    }
    public IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }

	public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        gm.StartCoroutine(gm.RespawnPlayer());
    }

    public static void KillEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}
