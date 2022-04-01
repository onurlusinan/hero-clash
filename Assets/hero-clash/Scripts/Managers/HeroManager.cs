using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    // Simple singleton Instance
    public static HeroManager Instance;

    [Header("Hero Leveling")]
    public int experiencePerLevel;

    [Header("HeroManager Config")]
    public GameObject heroPrefab;
    public int selectableHeroAmount;
    public Transform heroesParent;
    public List<int> selectedHeroIds; 

    public static event Action<int> selectedHeroAmountChanged;

    private Dictionary<int, Hero> _heroDict;
    private HeroBaseDataCollection _heroBaseDataCollection;

    

    private void Awake()
    {
        if (HeroManager.Instance == null)
            HeroManager.Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        _heroDict = new Dictionary<int, Hero>();
        selectedHeroIds = new List<int>();
        _heroBaseDataCollection = Resources.Load<HeroBaseDataCollection>("HeroBaseData/HeroBaseDataCollection");

        CreateHeroDict();

        SaveAllHeroes();
        LoadAllHeroes();
    }

    private void CreateHeroDict()
    {
        foreach (HeroBaseData baseData in _heroBaseDataCollection.GetCollection())
        {
            GameObject newHeroObject = Instantiate(heroPrefab, heroesParent);
            Hero newHero = newHeroObject.GetComponent<Hero>();
            newHero.LoadBaseData(baseData);
            newHero.RefreshHeroCardUI();

            _heroDict.Add(newHero.GetID(), newHero);
        }
    }

    private void LoadAllHeroes()
    {
        foreach (KeyValuePair<int, Hero> hero in _heroDict)
        {
            SaveSystem.LoadHero(hero.Value);
            hero.Value.RefreshHeroCardUI();
        }
    }

    private void SaveAllHeroes()
    {
        foreach (KeyValuePair<int, Hero> hero in _heroDict)
        {
            SaveSystem.SaveHero(hero.Value);
        }
    }
    
    /// <summary>
    /// Main select/deselect with id
    /// </summary>
    public void SelectHero(bool select, int id)
    {
        if (select)
        {
            selectedHeroIds.Add(id);

            if (selectedHeroIds.Count > selectableHeroAmount)
                _heroDict[selectedHeroIds[0]].Select(false);
        }
        else
        {
            selectedHeroIds.Remove(id);
            selectedHeroAmountChanged?.Invoke(selectedHeroIds.Count);
        }
        
        selectedHeroAmountChanged?.Invoke(selectedHeroIds.Count);
    }
}
