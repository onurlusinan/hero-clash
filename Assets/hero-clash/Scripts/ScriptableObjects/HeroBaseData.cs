using UnityEngine;

/// <summary>
/// Scriptable object that holds the base hero data to only initially load enemies from
/// </summary>
[CreateAssetMenu(fileName = "HeroBaseData", menuName = "ScriptableObjects/HeroBaseData")]
public class HeroBaseData : ScriptableObject
{
    [SerializeField]
    private int _id;

    [SerializeField]
    private string _heroName;

    [SerializeField]
    private float _health;

    [SerializeField]
    private float _attackPower;

    [SerializeField]
    private Sprite _defaultAvatar;

    public int GetID() => _id;
    public string GetHeroName() => _heroName;
    public float GetHealth() => _health;
    public float GetAttackPower() => _attackPower;
    public Sprite GetDefaultAvatar() => _defaultAvatar;
}

