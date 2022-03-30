using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeroData
{
    public int id;
    public string heroName;

    public float experience;
    public int level;

    public float health;
    public float attackPower;

    public bool isLocked;

    public HeroData(Hero hero)
    {
        id = hero.GetID();
        heroName = hero.GetName();
        experience = hero.GetExperience();
        level = hero.level;
        health = hero.health;
        attackPower = hero.attackPower;
        isLocked = hero.IsLocked();
    }
}
