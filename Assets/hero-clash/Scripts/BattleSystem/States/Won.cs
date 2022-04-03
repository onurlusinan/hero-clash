﻿using System.Collections;

internal class Won : State
{
    public Won(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    public override IEnumerator Start()
    {
        battleSystem.battleUI.PrintMessage("Battle Won.");
        HeroManager.Instance.LevelUpHeroes(battleSystem.GetWinnerHeroIDs());
        battleSystem.battleUI.SwitchPanel(PanelType.gameover);
        yield break;
    }
}