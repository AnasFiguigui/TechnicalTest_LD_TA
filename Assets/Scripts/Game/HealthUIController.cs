using TMPro;
using UnityEngine;

public class HealthUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerHPText;
    [SerializeField] private TextMeshProUGUI enemyHPText;
    [SerializeField] private HeroStatsSO heroStats;
    [SerializeField] private EnemyStatsSO enemyStats;

    private int playerCurrentHP;
    private int enemyCurrentHP;

    private void Start()
    {
        playerCurrentHP = heroStats.HP;
        enemyCurrentHP = enemyStats.HP;
        UpdateUI();
    }

    private void OnEnable()
    {
        EventBus.OnDamageDealt.AddListener(OnEnemyDamaged);
        EventBus.OnEnemyDefeated.AddListener(OnEnemyDefeatedHandler);
        EventBus.OnEnemyRespawned.AddListener(OnEnemyRespawnedHandler);
    }

    private void OnDisable()
    {
        EventBus.OnDamageDealt.RemoveListener(OnEnemyDamaged);
        EventBus.OnEnemyDefeated.RemoveListener(OnEnemyDefeatedHandler);
        EventBus.OnEnemyRespawned.RemoveListener(OnEnemyRespawnedHandler);
    }

    private void OnEnemyDamaged(int damage)
    {
        enemyCurrentHP -= damage;
        enemyCurrentHP = Mathf.Max(enemyCurrentHP, 0);
        UpdateUI();
    }

    private void OnEnemyDefeatedHandler()
    {
        enemyCurrentHP = 0;
        UpdateUI();
    }

    private void OnEnemyRespawnedHandler()
    {
        enemyCurrentHP = enemyStats.HP;
        UpdateUI();
    }

    private void UpdateUI()
    {
        playerHPText.text = $"Player HP: {playerCurrentHP}";
        enemyHPText.text = $"Enemy HP: {enemyCurrentHP}";
    }
}
