using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroBattleCard : BattleCard
{
    private Hero hero;  
    public static event Action<Hero> heroAttack;

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
        attributesPanel.RefreshPanelInfo(hero.level, hero.health, hero.attackPower);

        _totalHealth = hero.health;
        _currentHealth = _totalHealth;
    }
    public Hero GetHero() => hero;
    public void Attack()
    {
        heroAttack?.Invoke(hero);
    }
    public void SetInput(bool input)
    {
        longClickButton.SetInput(input);
    }
}
