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

    internal List<int> winnerIDs;

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
        winnerIDs = new List<int>();
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

    public void ClearHeroManager()
    {
        _heroDict.Clear();
        selectedHeroIds.Clear();
        winnerIDs.Clear();
    }
    public Hero GetHero(int id)
    {
        return _heroDict[id];
    }

    public void AddToHeroes(Hero hero)
    {
        _heroDict.Add(hero.GetID(), hero);
    }

    public void LevelUpHeroes(List<int> winnerIDs)
    {
        this.winnerIDs = winnerIDs;

        foreach (int id in winnerIDs)
        {
            _heroDict[id].LevelUp();
            Debug.Log(_heroDict[id].characterName + " now has +1 experience.");
        }

        SaveAllHeroes();
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
