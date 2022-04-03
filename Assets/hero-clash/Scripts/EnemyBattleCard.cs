using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBattleCard : BattleCard
{
    private EnemyBaseData enemyBaseData;

    public EnemyBaseData GetBaseData() => enemyBaseData;
    public override void RefreshCard()
    {
        healthBar.SetHealthBar(_currentHealth, _totalHealth);
        attributesPanel.RefreshPanelInfo(_currentHealth, _attackPower);
    }
    public void InitCard(EnemyBaseData enemyBaseData)
    {
        this.enemyBaseData = enemyBaseData;
        characterName.text = enemyBaseData.GetEnemyName();
        characterAvatar.sprite = enemyBaseData.GetDefaultAvatar();

        _totalHealth = enemyBaseData.GetHealth();
        _currentHealth = _totalHealth;
        _attackPower = enemyBaseData.GetAttackPower();

        attributesPanel.RefreshPanelInfo(enemyBaseData.GetHealth(), _attackPower);
    }
}
