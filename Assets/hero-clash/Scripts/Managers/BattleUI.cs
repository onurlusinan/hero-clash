using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class BattleUI : MonoBehaviour
{
    [Header("Battle-UI-Manager Config")]
    public Image overlay;
    public Text dialogueText;

    public List<HeroBattleCard> battleHeroes;

    private void Awake()
    {
        LoadHeroes();

        overlay.DOFade(0.0f, 0.5f).OnComplete(() =>
            overlay.gameObject.SetActive(false)
        );
    }

    private void LoadHeroes()
    {
        List<int> heroIDs = HeroManager.Instance.selectedHeroIds;

        for (int i = 0; i < heroIDs.Count; i++)
        {
            Hero newHero = HeroManager.Instance.GetHero(heroIDs[i]);
            battleHeroes[i].InitCard(newHero);
        }
    }

    public void PrintMessage(string message)
    {
        dialogueText.text = message;
    }

    public void SetAllInput(bool input)
    { 
        foreach(HeroBattleCard battleHero in battleHeroes)
            battleHero.SetInput(input);
    }
}
