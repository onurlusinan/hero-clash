using System.Collections;

internal class Lost : State
{
    public Lost(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    public override IEnumerator Start()
    {
        battleSystem.battleUI.PrintMessage("Battle lost.");
        battleSystem.battleUI.SwitchPanel(PanelType.gameover);
        yield break;
    }
}