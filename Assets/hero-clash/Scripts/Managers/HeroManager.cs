using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// SceneType enum that has values that corresponds to their necessary build indices
/// </summary>
public enum SceneType
{ 
    heroSelection,
    battleground
}

/// <summary>
/// Hero Manager handles everything related to the heroes
/// Holds the heroes in a dictionary with their ids
/// Levels them up when necessary, Saves and Loads them
/// </summary>
public class HeroManager : MonoBehaviour
{
    public static HeroManager Instance;

    [Header("Hero Leveling")]
    public int experiencePerLevel;
    public int battlesPerNewHero;
    public int initialHeroAmount;
    internal int totalBattles;

    [Header("Hero Manager Config")]
    public int selectableHeroAmount;
    public List<int> selectedHeroIds;
    public static event Action<int> selectedHeroAmountChanged;

    private Dictionary<int, Hero> _heroDict;
    internal List<int> winnerIDs;

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

    /// <summary>
    /// Gets the hero by id
    /// </summary>
    public Hero GetHero(int id) => _heroDict[id];

    /// <summary>
    /// Checks hero locks and turns them either on or off based on battles fought
    /// </summary>
    public void CheckLocks()
    {
        PlayerSaveData playerSaveData = SaveSystem.LoadPlayerData();
        
        if (playerSaveData == null)
            totalBattles = 0;
        else
            totalBattles = playerSaveData.battlesFought;
        
        int heroesToUnlock = totalBattles / battlesPerNewHero;
        if (heroesToUnlock == 0)
            heroesToUnlock = initialHeroAmount;
        else
            heroesToUnlock += initialHeroAmount;

        List<Hero> lockList = _heroDict.Values.ToList();
        for (int i = 0; i < lockList.Count; i++)
        {
            lockList[i].SetLock(!(i < heroesToUnlock));
            lockList[i].RefreshHeroCard();
        }
    }

    /// <summary>
    /// Loads all hero saves
    /// </summary>
    public void LoadAllHeroes()
    {
        foreach (KeyValuePair<int, Hero> hero in _heroDict)
            hero.Value.LoadHero();

        CheckLocks();
    }

    /// <summary>
    /// Saves all heroes
    /// </summary>
    private void SaveAllHeroes()
    {
        foreach (KeyValuePair<int, Hero> hero in _heroDict)
            hero.Value.SaveHero();
    }

    /// <summary>
    /// Clears the necessary data structures of Hero Manager
    /// Since it's the same singleton all session, needs empty data structures to fill them back
    /// </summary>
    public void ClearHeroManager()
    {
        _heroDict.Clear();
        selectedHeroIds.Clear();
        winnerIDs.Clear();
    }

    /// <summary>
    /// Adds hero to the hero dictionary _heroDict
    /// </summary>
    /// <param name="hero"></param>
    public void AddToHeroes(Hero hero)
    {
        _heroDict.Add(hero.GetID(), hero);
    }

    /// <summary>
    /// Levels up the winner heroes
    /// </summary>
    /// <param name="winnerIDs"> the list of ids of the winner heroes </param>
    public void LevelUpWinnerHeroes(List<int> winnerIDs)
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
