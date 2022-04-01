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

        CreateHeroDict();
        LoadAllHeroes();
    }

    private void CreateHeroDict()
    {
        foreach (Transform child in heroesParent)
        {
            Hero newHero = child.GetComponent<Hero>();
            _heroDict.Add(newHero.GetID(), newHero);
        }
    }

    private void LoadAllHeroes()
    {
        _heroBaseDataCollection = Resources.Load<HeroBaseDataCollection>("HeroBaseData/HeroBaseDataCollection");

        foreach(HeroBaseData baseData in _heroBaseDataCollection.GetCollection())
        {
            Hero hero = _heroDict[baseData.GetID()];
            hero.LoadBaseData(baseData);
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
