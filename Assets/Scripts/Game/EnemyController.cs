using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyStatsSO enemyStats;
    [SerializeField] private float respawnDelay = 3f;

    private int currentHP;
    private Vector3 initialPosition;
    private bool isDead = false;

    private void Start()
    {
        initialPosition = transform.position;
        ResetEnemy();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        int effectiveDamage = Mathf.Max(damage - enemyStats.DEF, 1);
        currentHP -= effectiveDamage;
        EventBus.OnDamageDealt.Invoke(effectiveDamage);

        if (currentHP <= 0)
        {
            currentHP = 0;
            EventBus.OnEnemyDefeated.Invoke();
            StartCoroutine(HandleDeath());
        }
    }

    private IEnumerator HandleDeath()
    {
        isDead = true;

        // Hide enemy (disable visuals + collider)
        GetComponent<SpriteRenderer>().enabled = false;
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        Canvas canvas = GetComponentInChildren<Canvas>();
        if (canvas != null) canvas.enabled = false;

        yield return new WaitForSeconds(respawnDelay);

        ResetEnemy();
    }

    private void ResetEnemy()
    {
        currentHP = enemyStats.HP;
        transform.position = initialPosition;
        isDead = false;

        // Re-enable visuals + collider + canvas
        GetComponent<SpriteRenderer>().enabled = true;
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = true;

        Canvas canvas = GetComponentInChildren<Canvas>();
        if (canvas != null) canvas.enabled = true;

        // Notify UI that enemy respawned
        EventBus.OnEnemyRespawned.Invoke();
    }

}
