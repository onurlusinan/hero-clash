using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        characterName.text = hero.characterName;
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
