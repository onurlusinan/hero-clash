using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    #region ATTRIBUTES
    private int _id;
    private string _heroName;

    private float _experience;
    public int level;

    public float health;
    public float attackPower;
    #endregion

    #region GETTERS - SETTERS
    public int GetID() => _id;
    public string GetName() => _heroName;
    public float GetExperience() => _experience;
    public void SetID(int id) => _id = id;
    public void SetHeroName(string name) => _heroName = name; 
    public void SetExperience(float experience) => _experience = experience; 

    #endregion

    #region SAVE-SYSTEM

    public void SaveHero()
    {
        SaveSystem.SaveHero(this);
    }

    public void LoadHero()
    {
        HeroData data = SaveSystem.LoadHero(this);

        SetID(data.id);
        SetHeroName(data.heroName);
        SetExperience(data.experience);
        level = data.level;
        health = data.health;
        attackPower = data.attackPower;
    }

    #endregion
}