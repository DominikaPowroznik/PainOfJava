using UnityEngine;

public class Enemy : MonoBehaviour {

    [System.Serializable]
    public class EnemyStats
    {
        public int maxHealth = 100;

        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public int damage = 1;

        public void Init()
        {
            curHealth = maxHealth;
        }
    }

    [SerializeField]
    private int fallBoundary = -10;

    [SerializeField]
    private EnemyStats enemyStats = new EnemyStats();

    [Header("Optional: ")]
    [SerializeField]
    private StatusIndicator statusIndicator;

    void Start()
    {
        enemyStats.Init();

        if(statusIndicator != null)
        {
            statusIndicator.SetHealth(enemyStats.curHealth, enemyStats.maxHealth);
        }
    }

    public void Update()
    {
        if (transform.position.y < fallBoundary)
        {
            DamageEnemy(999999);
        }
    }

    public void DamageEnemy(int damage)
    {
        enemyStats.curHealth -= damage;
        if (enemyStats.curHealth <= 0)
        {
            GameMaster.KillEnemy(this);
        }

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(enemyStats.curHealth, enemyStats.maxHealth);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Player player = collision.collider.GetComponent<Player>();
        if (player != null)
        {
            if(player.transform.position.x > (transform.position.x + GetComponent<BoxCollider2D>().size.x/2) && !GetComponent<SpriteRenderer>().flipX)
            {
                player.DamagePlayer(enemyStats.damage);
            }
            else if(player.transform.position.x < (transform.position.x - GetComponent<BoxCollider2D>().size.x / 2) && GetComponent<SpriteRenderer>().flipX)
            {
                player.DamagePlayer(enemyStats.damage);
            }
        }
    }
}
