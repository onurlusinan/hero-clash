using System.Collections;
using UnityEngine;

/// <summary>
/// The state called when the game is lost
/// </summary>
internal class Lost : State
{
    public Lost(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    /// <summary>
    /// Saves fought battles then Switches to Lose UI Panel
    /// </summary>
    public override IEnumerator Start()
    {
        battleSystem.battleUI.PrintMessage("Battle lost.");

        int battlesFought = HeroManager.Instance.totalBattles +1;
        SaveSystem.SavePlayerData(battlesFought);

        yield return new WaitForSeconds(1f);

        battleSystem.battleUI.SwitchPanel(PanelType.lose);
        yield break;
    }
}