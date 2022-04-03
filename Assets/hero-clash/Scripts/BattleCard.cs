using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public enum InteractionType
{ 
    attack,
    damage
}

public abstract class BattleCard : MonoBehaviour
{
    [Header("UI-Config")]
    public Text characterName;
    public Image characterAvatar;
    public AttributesPanel attributesPanel;
    public HealthBar healthBar;
    public LongClickButton longClickButton;
    public DamageDisplay damageDisplay;

    protected bool _isDead;
    protected float _totalHealth;
    protected float _currentHealth;
    protected float _attackPower;

    public abstract void RefreshCard();
    public virtual float GetCurrentHealth() => _currentHealth;
    public virtual float GetTotalHealth() => _totalHealth;
    public virtual float GetAttackPower() => _attackPower;
    public virtual bool IsDead() => _isDead;
    public virtual bool Damage(float amount)
    {
        AnimateBattleCard(InteractionType.damage);

        _currentHealth = _currentHealth - amount;
        _isDead = _currentHealth <= 0;
        return _isDead;
    }
    public virtual void AnimateBattleCard(InteractionType animType)
    {
        switch (animType)
        {
            case InteractionType.attack:
                AttackAnim();
                break;
            case InteractionType.damage:
                DamageAnim();
                break;
        }
    }

    private void AttackAnim()
    {
        float originalPosY = transform.position.y;
        Sequence attackSequence = DOTween.Sequence();
        attackSequence.Append(transform.DOMoveY(originalPosY - 0.25f, 0.1f))
                      .Append(transform.DOMoveY(originalPosY, 0.1f))
                      .SetEase(Ease.Linear);
    }

    private void DamageAnim()
    {
        float originalPosX = transform.position.x;
        Sequence attackSequence = DOTween.Sequence();
        attackSequence.Append(transform.DOMoveX(originalPosX + 0.5f, 0.1f))
                      .Append(transform.DOMoveX(originalPosX - 0.5f, 0.1f))
                      .Append(transform.DOMoveX(originalPosX + 0.25f, 0.1f))
                      .Append(transform.DOMoveX(originalPosX - 0.25f, 0.1f))
                      .Append(transform.DOMoveX(originalPosX + 0.1f, 0.1f))
                      .Append(transform.DOMoveX(originalPosX - 0.1f, 0.1f))
                      .Append(transform.DOMoveX(originalPosX, 0.1f))
                      .SetEase(Ease.Linear);
    }
}