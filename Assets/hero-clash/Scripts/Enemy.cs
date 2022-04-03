using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    public HealthBar healthBar;
    private float _totalHealth;

    public float GetTotalHealth() => _totalHealth;

    public override void RefreshCharacterCard()
    {
        characterNameText.text = characterName;
        attributesPanel.RefreshPanelInfo(health, attackPower);
        characterAvatar.sprite = defaultAvatarSprite;
    }

    public void LoadBaseData(EnemyBaseData enemyBaseData)
    {
        characterName = enemyBaseData.GetEnemyName();
        health = enemyBaseData.GetHealth();
        attackPower = enemyBaseData.GetAttackPower();
        defaultAvatarSprite = enemyBaseData.GetDefaultAvatar();
        _totalHealth = health;

        RefreshCharacterCard();
    }

    public bool Damage(float amount)
    {
        health = health - amount;
        return health <= 0;
    }
}
