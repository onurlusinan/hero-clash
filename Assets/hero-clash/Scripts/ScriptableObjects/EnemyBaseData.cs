using UnityEngine;

/// <summary>
/// Scriptable object that holds the enemy data to load enemies from
/// </summary>
[CreateAssetMenu(fileName = "EnemyBaseData", menuName = "ScriptableObjects/EnemyBaseData")]
public class EnemyBaseData : ScriptableObject
{
    [SerializeField]
    private string _enemyName;

    [SerializeField]
    private float _health;

    [SerializeField]
    private float _attackPower;

    [SerializeField]
    private Sprite _defaultAvatar;

    public string GetEnemyName() => _enemyName;
    public float GetHealth() => _health;
    public float GetAttackPower() => _attackPower;
    public Sprite GetDefaultAvatar() => _defaultAvatar;
}
