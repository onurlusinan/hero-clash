using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;
using HeroClash.Audio;

namespace HeroClash.UserInterface
{
    /// <summary>
    /// Battle Card interactions
    /// </summary>
    public enum InteractionType
    { 
        attack,
        damage
    }

    /// <summary>
    /// Handles the character cards on the battleground
    /// </summary>
    public abstract class BattleCard : MonoBehaviour
    {
        [Header("UI-Config")]
        public Text characterName;
        public Image characterAvatar;
        public AttributesPanel attributesPanel;
        public HealthBar healthBar;
        public LongClickButton longClickButton;
        public DamageDisplay damageDisplay;
        public CanvasGroup visuals;

        protected bool _isDead;
        protected float _totalHealth;
        protected float _currentHealth;
        protected float _attackPower;

        public virtual float GetCurrentHealth() => _currentHealth;
        public virtual float GetTotalHealth() => _totalHealth;
        public virtual float GetAttackPower() => _attackPower;
        public virtual bool IsDead() => _isDead;

        /// <summary>
        /// Refreshes the info on the battle card after an interaction
        /// </summary>
        public abstract void RefreshCard();

        /// <summary>
        /// Damages the battle card
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public virtual bool Damage(float amount)
        {
            AnimateBattleCard(InteractionType.damage);

            _currentHealth = _currentHealth - amount;
            _isDead = _currentHealth <= 0;
        
            return _isDead;
        }

        public virtual void FadeCard(bool fade)
        {
            if (fade)
                visuals.DOFade(0.75f, 0.2f);
            else
                visuals.DOFade(1.0f, 0.2f);
        }

        /// <summary>
        /// Animates the battle card based on interaction type
        /// </summary>
        public virtual void AnimateBattleCard(InteractionType interactionType)
        {
            switch (interactionType)
            {
                case InteractionType.attack:
                    AttackAnim();
                    break;
                case InteractionType.damage:
                    SoundManager.Instance.Play(Sounds.punch);
                    DamageAnim();
                    break;
            }
        }

        /// <summary>
        /// The attack animation of the battle cards
        /// </summary>
        private void AttackAnim()
        {
            float originalPosY = transform.position.y;
            Sequence attackSequence = DOTween.Sequence();
            attackSequence.Append(transform.DOMoveY(originalPosY - 0.25f, 0.1f))
                          .Append(transform.DOMoveY(originalPosY, 0.1f))
                          .SetEase(Ease.Linear);
        }

        /// <summary>
        /// The damage animation of the battle cards
        /// </summary>
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
}