using System.Collections;

using HeroClash.UserInterface;

namespace HeroClash.CombatSystem
{
    public abstract class State
    {
        protected BattleSystem battleSystem;

        protected State(BattleSystem battleSystem)
        {
            this.battleSystem = battleSystem;
        }

        public virtual IEnumerator Start()
        {
            yield break;
        }

        public virtual IEnumerator Attack(HeroBattleCard heroBattleCard)
        {
            yield break;
        }
    }
}
