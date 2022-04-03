using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;
using UnityEngine.SceneManagement;

public enum PanelType
{ 
    battle,
    gameover
}

public class BattleUI : MonoBehaviour
{
    [Header("Battle-UI-Manager Config")]
    public Image overlay;
    public Text dialogueText;

    public List<HeroBattleCard> battleHeroes;

    [Header("UI-Panels")]
    public CanvasGroup battlePanel;
    public CanvasGroup gameoverPanel;
    private PanelType _currentPanel;

    private void Awake()
    {
        LoadHeroes();

        _currentPanel = PanelType.battle;
        SwitchPanel(_currentPanel);

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

    public void SwitchPanel(PanelType panelType)
    {
        switch (_currentPanel)
        {
            case PanelType.battle:
                battlePanel.DOFade(0.0f, 0.2f);
                battlePanel.interactable = false;
                break;
            case PanelType.gameover:
                gameoverPanel.DOFade(0.0f, 0.2f).OnComplete(() =>
                    gameoverPanel.gameObject.SetActive(false)
                );
                gameoverPanel.interactable = false;
                break;
        }
        switch (panelType)
        {
            case PanelType.battle:
                battlePanel.DOFade(1.0f, 0.2f);
                battlePanel.interactable = true;
                break;
            case PanelType.gameover:
                gameoverPanel.gameObject.SetActive(true);
                gameoverPanel.DOFade(1.0f, 0.2f);
                gameoverPanel.interactable = true;
                break;
        }
        _currentPanel = panelType;
    }

    public void BackToHeroSelection()
    {
        overlay.gameObject.SetActive(true);
        overlay.DOFade(1.0f, 0.2f).OnComplete(() =>
                    SceneManager.LoadScene((int)SceneType.heroSelection)
        );
    }
}
