using System.Collections;

internal class Lost : State
{
    public Lost(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    public override IEnumerator Start()
    {
        battleSystem.battleUI.PrintMessage("Battle lost.");
        HeroManager.Instance.LevelUpHeroes(battleSystem.GetWinnerHeroIDs());
        battleSystem.battleUI.SwitchPanel(PanelType.gameover);
        yield break;
    }
}