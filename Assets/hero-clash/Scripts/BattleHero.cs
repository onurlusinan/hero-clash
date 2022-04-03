using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHero : MonoBehaviour
{
    [Header("UI-Config")]
    public Text heroName;
    public Image characterAvatar;
    public AttributesPanel attributesPanel;
    public HealthBar healthBar;
    public LongClickButton button;

    private Hero hero;
    private float _currentHealth;

    public static event Action<Hero> heroAttack;

    public void InitBattleHero(Hero hero)
    {
        this.hero = hero;
        heroName.text = hero.characterName;
        characterAvatar.sprite = hero.defaultAvatarSprite;
        attributesPanel.RefreshPanelInfo(hero.level, hero.health, hero.attackPower);
        _currentHealth = hero.health;
    }

    public void RefreshBattleHero()
    {
        healthBar.SetHealthBar(_currentHealth, hero.health);
        attributesPanel.RefreshPanelInfo(hero.level, _currentHealth, hero.attackPower);
    }

    public bool Damage(float amount)
    {
        _currentHealth = _currentHealth - amount;
        return _currentHealth <= 0;
    }

    public Hero GetHero() => hero;
    public float GetCurrentHealth() => _currentHealth;

    public void Attack()
    {
        heroAttack?.Invoke(hero);
    }

    public void SetInput(bool input)
    {
        button.SetInput(input);
    }
}
