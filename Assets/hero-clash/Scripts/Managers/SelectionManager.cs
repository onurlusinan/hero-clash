using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using DG.Tweening;

public class SelectionManager : MonoBehaviour
{
    [Header("UIManager Config")]
    public Button battleButton;
    public Image overlay;

    [Header("Hero loading")]
    public GameObject heroPrefab;
    public Transform heroesParent;

    private HeroBaseDataCollection _heroBaseDataCollection;

    private void Awake()
    {
        HeroManager.selectedHeroAmountChanged += OnSelectedHeroAmountChanged;
        
        battleButton.interactable = false;

        _heroBaseDataCollection = Resources.Load<HeroBaseDataCollection>("HeroBaseData/HeroBaseDataCollection");

        InstantiateHeroes();

        overlay.DOFade(0.0f, 0.5f).OnComplete(() =>
            overlay.gameObject.SetActive(false)
        );
    }

    private void OnDestroy()
    {
        HeroManager.selectedHeroAmountChanged -= OnSelectedHeroAmountChanged;
    }

    private void OnSelectedHeroAmountChanged(int count)
    {
        bool isInteractable = (count == HeroManager.Instance.selectableHeroAmount);
        battleButton.interactable = isInteractable;
    }

    /// <summary>
    /// Instantiates the hero prefabs based on collected hero base data
    /// </summary>
    private void InstantiateHeroes()
    {
        HeroManager.Instance.ClearHeroManager();

        foreach (HeroBaseData baseData in _heroBaseDataCollection.GetCollection())
        {
            GameObject newHeroObject = Instantiate(heroPrefab, heroesParent);
            Hero newHero = newHeroObject.GetComponent<Hero>();
            newHero.LoadBaseData(baseData);

            HeroManager.Instance.AddToHeroes(newHero);
        }

        HeroManager.Instance.LoadAllHeroes();
    }

    public void BattleButton()
    {
        overlay.gameObject.SetActive(true);
        overlay.DOFade(1.0f, 0.2f).OnComplete(() =>
                    SceneManager.LoadScene((int)SceneType.battleground)
        ) ;
    }
}
