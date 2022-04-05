using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using DG.Tweening;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance;

    [Header("UIManager Config")]
    public Button battleButton;
    public Image overlay;

    [Header("Hero loading")]
    public GameObject heroSelectionCardPrefab;
    public Transform heroesParent;

    [Header("Selection Config")]
    public int selectableHeroAmount;
    public List<int> selectedHeroIDs;

    private HeroBaseDataCollection _heroBaseDataCollection;
    private Dictionary<int, HeroSelectionCard> _heroSelectionCardDict;

    private void Awake()
    {
        if (SelectionManager.Instance == null)
            SelectionManager.Instance = this;
        else
            Destroy(gameObject);

        battleButton.interactable = false;
        selectedHeroIDs = new List<int>();
        _heroSelectionCardDict = new Dictionary<int, HeroSelectionCard>();

        _heroBaseDataCollection = Resources.Load<HeroBaseDataCollection>("HeroBaseData/HeroBaseDataCollection");

        InstantiateHeroes();

        overlay.DOFade(0.0f, 0.5f).OnComplete(() =>
            overlay.gameObject.SetActive(false)
        );
    }

    public HeroSelectionCard GetHeroSelectionCard(int id) => _heroSelectionCardDict[id];

    private void SetBattleButton(int count)
    {
        bool isInteractable = (count == selectableHeroAmount);
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
            GameObject heroSelectionCardObj = Instantiate(heroSelectionCardPrefab, heroesParent);
            HeroSelectionCard newHeroSelectionCard = heroSelectionCardObj.GetComponent<HeroSelectionCard>();

            Hero newHero = new Hero();
            newHero.LoadBaseData(baseData);
            HeroManager.Instance.AddToHeroes(newHero);

            newHeroSelectionCard.InitHeroCard(newHero);
            _heroSelectionCardDict.Add(newHero.GetID(), newHeroSelectionCard);
        }

        HeroManager.Instance.LoadAllHeroes();
    }

    /// <summary>
    /// Main select/deselect with id
    /// </summary>
    public void SelectHero(bool select, int id)
    {
        if (select)
        {
            selectedHeroIDs.Add(id);

            if (selectedHeroIDs.Count > selectableHeroAmount)
                _heroSelectionCardDict[selectedHeroIDs[0]].Select(false);
        }
        else
            selectedHeroIDs.Remove(id);

        SetBattleButton(selectedHeroIDs.Count);
    }

    public void BattleButton()
    {
        HeroManager.Instance.selectedHeroes = selectedHeroIDs;

        overlay.gameObject.SetActive(true);
        overlay.DOFade(1.0f, 0.2f).OnComplete(() =>
                    SceneManager.LoadScene((int)SceneType.battleground)
        ) ;
    }
}
