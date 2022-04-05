namespace HeroClash.UserInterface
{
    /// <summary>
    /// The battle card for the enemy on the battleground
    /// </summary>
    public class EnemyBattleCard : BattleCard
    {
        private EnemyBaseData enemyBaseData;
        public EnemyBaseData GetBaseData() => enemyBaseData;

        /// <summary>
        /// Fills Card with enemy base data
        /// </summary>
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

        public override void RefreshCard()
        {
            healthBar.SetHealthBar(_currentHealth, _totalHealth);
            attributesPanel.RefreshPanelInfo(_currentHealth, _attackPower);
        }
    }
}