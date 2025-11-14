using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Stats/EnemyStats")]
public class EnemyStatsSO : ScriptableObject
{
    public int HP;
    public int ATK;
    public int DEF;
}
