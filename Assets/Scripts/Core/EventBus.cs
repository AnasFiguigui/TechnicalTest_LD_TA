using UnityEngine;
using UnityEngine.Events;

public static class EventBus
{
    public static UnityEvent<int> OnDamageDealt = new UnityEvent<int>();
    public static UnityEvent OnEnemyDefeated = new UnityEvent();
    public static UnityEvent OnEnemyRespawned = new UnityEvent();
}
