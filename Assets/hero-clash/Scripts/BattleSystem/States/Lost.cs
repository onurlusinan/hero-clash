using System.Collections;

internal class Lost : State
{
    public Lost(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    public override IEnumerator Start()
    {
        battleSystem.battleUI.PrintMessage("Battle lost.");

        int battlesFought = HeroManager.Instance.battlesFought +1;
        SaveSystem.SavePlayerData(battlesFought);

        battleSystem.battleUI.SwitchPanel(PanelType.lose);
        yield break;
    }
}