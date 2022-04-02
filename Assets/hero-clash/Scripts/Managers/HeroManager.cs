using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneType
{ 
    heroSelection,
    battleground
}

public class HeroManager : MonoBehaviour
{
    // Simple singleton Instance
    public static HeroManager Instance;

    [Header("Hero Leveling")]
    public int experiencePerLevel;

    [Header("HeroManager Config")]
    public int selectableHeroAmount;
    public List<int> selectedHeroIds;
    public static event Action<int> selectedHeroAmountChanged;

    private Dictionary<int, Hero> _heroDict;

    private void Awake()
    {
        if (HeroManager.Instance == null)
            HeroManager.Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        _heroDict = new Dictionary<int, Hero>();
        selectedHeroIds = new List<int>();
    }

    public void LoadAllHeroes()
    {
        foreach (KeyValuePair<int, Hero> hero in _heroDict)
            hero.Value.LoadHero();
    }

    private void SaveAllHeroes()
    {
        foreach (KeyValuePair<int, Hero> hero in _heroDict)
            hero.Value.SaveHero();
    }

    public Hero GetHero(int id)
    {
        return _heroDict[id];
    }

    public void AddToHeroes(Hero hero)
    {
        _heroDict.Add(hero.GetID(), hero);
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
