using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroBattleCard : BattleCard
{
    private Hero hero;  
    public static event Action<HeroBattleCard> heroAttack;

    public override void RefreshCard()
    {
        healthBar.SetHealthBar(_currentHealth, hero.health);
        attributesPanel.RefreshPanelInfo(hero.level, _currentHealth, hero.attackPower);
    }
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

    public Hero GetHero() => hero;
    public void Attack()
    {
        if (_isDead)
            return;

        AnimateBattleCard(InteractionType.attack);
        heroAttack?.Invoke(this);
    }
    public void SetInput(bool input)
    {
        longClickButton.SetInput(input);
    }
}
