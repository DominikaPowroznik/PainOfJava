using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    [System.Serializable]
	public class PlayerStats
    {
        public int maxHealth = 100;

        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        private static int _wonPoints = 0;
        public static int WonPoints
        {
            get { return _wonPoints; }
            set { _wonPoints = value; }
        }

        private static int _lostPoints = 0;
        public static int LostPoints
        {
            get { return _lostPoints; }
            set { _lostPoints = value; }
        }

        public int damage = 5;

        public void Init()
        {
            curHealth = maxHealth;
        }

        public static int GetAllPoints()
        {
            return WonPoints + LostPoints;
        }
    }

    public PlayerStats playerStats = new PlayerStats();

    public int fallBoundary = -10;

    [SerializeField]
    private StatusIndicator statusIndicator;

    [SerializeField]
    private GameObject pauseUI;

    void OnLevelWasLoaded(int level)
    {
        PlayerStats.WonPoints = 0;
        PlayerStats.LostPoints = 0;
        playerStats.Init();
        statusIndicator.SetHealth(playerStats.curHealth, playerStats.maxHealth);
    }

    public void Update()
    {
        if(transform.position.y < fallBoundary)
        {
            DamagePlayer(999999);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //pausing the game
            Time.timeScale = 0.0f;

            pauseUI.SetActive(true);
        }
    }

    public void DamagePlayer(int damage)
    {
        playerStats.curHealth -= damage;
        if(playerStats.curHealth <= 0)
        {
            GameMaster.KillPlayer(this);
        }

        statusIndicator.SetHealth(playerStats.curHealth, playerStats.maxHealth);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        if (enemy != null)
        {
            if(transform.position.y > enemy.transform.FindChild("CeilingCheck").transform.position.y)
            {
                enemy.DamageEnemy(playerStats.damage);
            }
        }
    }
}
