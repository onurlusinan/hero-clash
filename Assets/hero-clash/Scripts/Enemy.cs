using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    public override void RefreshCharacterCard()
    {
        characterNameText.text = characterName;
        attributePanel.RefreshPanelInfo(health, attackPower);
        characterAvatar.sprite = defaultAvatarSprite;
    }

    public void LoadBaseData(EnemyBaseData enemyBaseData)
    {
        characterName = enemyBaseData.GetEnemyName();
        health = enemyBaseData.GetHealth();
        attackPower = enemyBaseData.GetAttackPower();
        defaultAvatarSprite = enemyBaseData.GetDefaultAvatar();

        RefreshCharacterCard();
    }
}
