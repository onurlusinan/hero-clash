using UnityEngine;

namespace HeroClash.CombatSystem
{
    public class StateMachine : MonoBehaviour
    {
        protected State battleState;

        public void SetState(State state)
        {
            battleState = state;
            StartCoroutine(battleState.Start());
        }
    }
}