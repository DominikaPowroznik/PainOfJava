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

    void Start()
    {
        playerStats.Init();

        if (statusIndicator == null)
        {
            Debug.LogError("PLAYER: No StatusIndicator object referenced!");
        }
        else
        {
            statusIndicator.SetHealth(playerStats.curHealth, playerStats.maxHealth);
        }
    }

    public void Update()
    {
        if(transform.position.y < fallBoundary)
        {
            DamagePlayer(999999);
        }
    }

    public void DamagePlayer(int damage)
    {
        playerStats.curHealth -= damage;
        if(playerStats.curHealth <= 0)
        {
            GameMaster.KillPlayer(this);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        if(enemy != null)
        {
            DamagePlayer(enemy.enemyStats.damage);
        }

        statusIndicator.SetHealth(playerStats.curHealth, playerStats.maxHealth);
    }
}
