using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPanel : MonoBehaviour
{
    public GameObject heroLevelUpCard;
    public Transform levelUpList;

    private void OnEnable()
    {
        PopulateBoard();
    }

    private void PopulateBoard()
    {
        foreach (int id in HeroManager.Instance.winnerIDs)
        {
            GameObject heroLevelUpObject = Instantiate(heroLevelUpCard, levelUpList);
            HeroLevelUpCard newHeroLevelUpCard = heroLevelUpObject.GetComponent<HeroLevelUpCard>();
            Hero winnerHero = HeroManager.Instance.GetHero(id);
            newHeroLevelUpCard.SetCard(winnerHero);
        }
    }
}
