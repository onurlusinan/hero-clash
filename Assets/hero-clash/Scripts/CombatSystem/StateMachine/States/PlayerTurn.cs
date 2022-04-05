using System.Collections;
using UnityEngine;

using HeroClash.UserInterface;

namespace HeroClash.CombatSystem
{
    internal class PlayerTurn : State
    {
        public PlayerTurn(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        /// <summary>
        /// Enables player input
        /// </summary>
        public override IEnumerator Start()
        {
            battleSystem.battleUI.PrintMessage("Tap on a hero to attack with.");
            battleSystem.battleUI.SetBackImages(true);
            battleSystem.battleUI.SetAllInput(true);
            yield return new WaitForSeconds(1f);
        }

        /// <summary>
        /// Attacks the enemy then disables player input
        /// </summary>
        /// <param name="heroBattleCard"> The hero battle card to attack with </param>
        public override IEnumerator Attack(HeroBattleCard heroBattleCard)
        {
            string heroName = heroBattleCard.GetHero().GetName();
            float attackPower = heroBattleCard.GetAttackPower();
            EnemyBattleCard enemyBattleCard = battleSystem.enemyBattleCard;

            bool isDead = enemyBattleCard.Damage(heroBattleCard.GetAttackPower());

            battleSystem.battleUI.PrintMessage("The hero " + heroName
                + " attacked " + enemyBattleCard.GetBaseData().GetEnemyName()
                + " with Attack Power: " + heroBattleCard.GetAttackPower());

            enemyBattleCard.damageDisplay.ShowText(InteractionType.damage, attackPower);
            heroBattleCard.damageDisplay.ShowText(InteractionType.attack, attackPower);

            battleSystem.enemyBattleCard.RefreshCard();

            battleSystem.battleUI.SetAllInput(false);

            yield return new WaitForSeconds(2f);

            if (isDead)
                battleSystem.SetState(new Won(battleSystem));
            else
                battleSystem.SetState(new EnemyTurn(battleSystem));
        }
    }
}