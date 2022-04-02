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
    public LongClickButton button;

    private Hero hero;

    public static event Action<Hero> heroAttack;

    public void LoadBattleHero(Hero hero)
    {
        this.hero = hero;
        heroName.text = hero.characterName;
        characterAvatar.sprite = hero.defaultAvatarSprite;
        attributesPanel.RefreshPanelInfo(hero.level, hero.health, hero.attackPower);
    }

    public Hero GetHero() => hero;

    public void Attack()
    {
        Debug.Log("Attack fired from hero " + hero.characterName);
        heroAttack?.Invoke(hero);
    }

    public void SetInput(bool input)
    {
        button.SetInput(input);
    }
}
