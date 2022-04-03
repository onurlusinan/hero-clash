using UnityEngine;

[CreateAssetMenu(fileName = "HeroBaseData", menuName = "ScriptableObjects/HeroBaseData")]
public class HeroBaseData : ScriptableObject
{
    [SerializeField]
    private int _id;

    [SerializeField]
    private string _heroName;

    [SerializeField]
    private int _experience;

    [SerializeField]
    private float _health;

    [SerializeField]
    private float _attackPower;

    [SerializeField]
    private Sprite _defaultAvatar;

    public int GetID() => _id;
    public string GetHeroName() => _heroName;
    public int GetExperience() => _experience;
    public float GetHealth() => _health;
    public float GetAttackPower() => _attackPower;
    public Sprite GetDefaultAvatar() => _defaultAvatar;
}

