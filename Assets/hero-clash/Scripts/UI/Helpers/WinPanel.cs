using HeroClash.HeroSystem;
using System.Collections.Generic;
using UnityEngine;

namespace HeroClash.UserInterface
{
    public class WinPanel : MonoBehaviour
    {
        public GameObject heroLevelUpCard;
        public Transform levelUpList;

        private void OnEnable()
        {
            PopulateBoard(HeroManager.Instance.winnerIDs);
        }

        /// <summary>
        /// Populates the final board dynamically with winner heroes using the prefab
        /// </summary>
        private void PopulateBoard(List<int> winnerIDs)
        {
            foreach (int id in winnerIDs)
            {
                GameObject heroLevelUpObject = Instantiate(heroLevelUpCard, levelUpList);
                HeroLevelUpCard newHeroLevelUpCard = heroLevelUpObject.GetComponent<HeroLevelUpCard>();
                Hero winnerHero = HeroManager.Instance.GetHero(id);
                newHeroLevelUpCard.SetCard(winnerHero);
            }
        }
    }
}