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

    private Dictionary<int, Hero> _heroDict;
    private List<int> _selectedHeroIDs;

    private void Awake()
    {
        if (HeroManager.Instance == null)
            HeroManager.Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        _heroDict = new Dictionary<int, Hero>();
        _selectedHeroIDs = new List<int>();

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

    public void SelectHero(Hero hero)
    {
        
    }

    public void DeselectHero(Hero hero)
    {

    }
}
