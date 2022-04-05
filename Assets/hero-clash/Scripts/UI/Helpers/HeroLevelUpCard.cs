using UnityEngine;
using UnityEngine.UI;

using HeroClash.HeroSystem;

namespace HeroClash.UserInterface
{
    public class HeroLevelUpCard : MonoBehaviour
    {
        public Text heroName;

        public void SetCard(Hero hero)
        {
            heroName.text = hero.GetName();
        }
    }
}