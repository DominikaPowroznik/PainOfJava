using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    [System.Serializable]
    public class EnemyStats
    {
        public int health = 100;

        public int damage = 20;
    }

    public EnemyStats enemyStats = new EnemyStats();

    public int fallBoundary = -10;

    public void Update()
    {
        if (transform.position.y < fallBoundary)
        {
            DamageEnemy(999999);
        }
    }

    public void DamageEnemy(int damage)
    {
        enemyStats.health -= damage;
        if (enemyStats.health <= 0)
        {
            GameMaster.KillEnemy(this);
        }
    }
}
