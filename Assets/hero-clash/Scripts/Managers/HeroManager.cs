using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    // Simple singleton Instance
    public static HeroManager Instance;

    [Header("HeroManager Config")]
    public int selectableHeroAmount;
    public Transform heroesParent;
    public List<int> selectedHeroIds;

    private Dictionary<int, Hero> _heroDict;
    public static event Action<int> selectedHeroAmountChanged;

    private void Awake()
    {
        if (HeroManager.Instance == null)
            HeroManager.Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        _heroDict = new Dictionary<int, Hero>();
        selectedHeroIds = new List<int>();

        CreateHeroList();
    }

    private void CreateHeroList()
    {
        foreach (Transform child in heroesParent)
        {
            Hero newHero = child.GetComponent<Hero>();
            _heroDict.Add(newHero.GetID(), newHero);
        }
    }

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
