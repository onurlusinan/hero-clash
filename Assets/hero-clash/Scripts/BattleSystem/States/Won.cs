using System.Collections;

internal class Won : State
{
    public Won(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    public override IEnumerator Start()
    {
        battleSystem.battleUI.PrintMessage("Battle Won.");
        HeroManager.Instance.LevelUpHeroes(battleSystem.GetWinnerHeroIDs());

        int battlesFought = HeroManager.Instance.battlesFought +1;
        SaveSystem.SavePlayerData(battlesFought);

        battleSystem.battleUI.SwitchPanel(PanelType.win);
        yield break;
    }
}