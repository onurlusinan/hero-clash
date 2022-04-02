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

    private Hero hero;

    public void LoadBattleHero(Hero hero)
    {
        this.hero = hero;
        heroName.text = hero.characterName;
        characterAvatar.sprite = hero.defaultAvatarSprite;
        attributesPanel.RefreshPanelInfo(hero.level, hero.health, hero.attackPower);
    }

    public Hero GetHero() => hero;
}
