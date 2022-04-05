using System.Collections;
using UnityEngine;

/// <summary>
/// The begin state that starts the entire battle
/// </summary>
internal class Begin : State
{
    public Begin(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    /// <summary>
    /// /// Sets input false then starts playerTurn state
    /// </summary>
    public override IEnumerator Start()
    {
        battleSystem.battleUI.SetAllInput(false);
        battleSystem.battleUI.PrintMessage("Another epic battle begins!");

        yield return new WaitForSeconds(3f);
        battleSystem.SetState(new PlayerTurn(battleSystem));
    }
}
