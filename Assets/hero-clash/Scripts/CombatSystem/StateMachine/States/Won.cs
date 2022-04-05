using System.Collections;

using HeroClash.HeroSystem;
using HeroClash.PersistentData;
using HeroClash.UserInterface;

namespace HeroClash.CombatSystem
{
    internal class Won : State
    {
        public Won(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        /// <summary>
        /// Saves total battle amount
        /// Gets the winner ids from battle system, sends them to HeroManager for level up
        /// Switches to the Win panel
        /// </summary>
        public override IEnumerator Start()
        {
            SoundManager.Instance.Play(Sounds.win);

            int totalBattles = HeroManager.Instance.totalBattles + 1;
            SaveSystem.SavePlayerData(totalBattles);

            battleSystem.battleUI.PrintMessage("Battle Won.");
            HeroManager.Instance.LevelUpWinnerHeroes(battleSystem.GetWinnerHeroIDs());

            battleSystem.battleUI.ResetBackImages();
            battleSystem.battleUI.SwitchPanel(PanelType.win);
            yield break;
        }
    }
}