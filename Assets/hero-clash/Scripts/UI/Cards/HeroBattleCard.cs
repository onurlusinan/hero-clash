using System;
using HeroClash.HeroSystem;

namespace HeroClash.UserInterface
{
    public class HeroBattleCard : BattleCard
    {
        private Hero hero;
        public static event Action<HeroBattleCard> heroAttack;

        public Hero GetHero() => hero;

        /// <summary>
        /// Enables or disables player input for the battle card
        /// </summary>
        public void SetInput(bool input) => longClickButton.SetInput(input);

        /// <summary>
        /// Fills Card using a Hero
        /// </summary>
        public void InitCard(Hero hero)
        {
            this.hero = hero;
            characterName.text = hero.GetName(); ;
            characterAvatar.sprite = hero.defaultAvatarSprite;
            _totalHealth = hero.health;
            _attackPower = hero.attackPower;
            _currentHealth = _totalHealth;

            attributesPanel.RefreshPanelInfo(hero.level, _currentHealth, _attackPower);
        }

        public override void RefreshCard()
        {
            healthBar.SetHealthBar(_currentHealth, hero.health);
            attributesPanel.RefreshPanelInfo(hero.level, _currentHealth, hero.attackPower);
        }

        public override bool Damage(float amount)
        {
            AnimateBattleCard(InteractionType.damage);

            _currentHealth = _currentHealth - amount;
            _isDead = _currentHealth <= 0;

            FadeCard(_isDead);

            return _isDead;
        }

        /// <summary>
        /// For the button, invokes event to Battle System
        /// </summary>
        public void Attack()
        {
            if (_isDead)
                return;

            AnimateBattleCard(InteractionType.attack);
            heroAttack?.Invoke(this);
        }


    }
}