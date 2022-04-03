using UnityEngine;
using UnityEngine.UI;

public abstract class BattleCard : MonoBehaviour
{
    [Header("UI-Config")]
    public Text characterName;
    public Image characterAvatar;
    public AttributesPanel attributesPanel;
    public HealthBar healthBar;
    public LongClickButton longClickButton;

    protected float _totalHealth;
    protected float _currentHealth;
    protected float _attackPower;

    public abstract void RefreshCard();
    public virtual float GetCurrentHealth() => _currentHealth;
    public virtual float GetTotalHealth() => _totalHealth;
    public virtual float GetAttackPower() => _attackPower;
    public virtual bool Damage(float amount)
    {
        _currentHealth = _currentHealth - amount;
        return _currentHealth <= 0;
    }
}