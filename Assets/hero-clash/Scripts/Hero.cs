using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    #region ATTRIBUTES
    [Header("Hero Attributes")]
    private int _id;
    [SerializeField]private string _heroName;
    private float _experience;
    public int level;
    public float health;
    public float attackPower;

    [SerializeField] private bool _isLocked;

    [Header("UI Config")]
    public Image heroAvatar;
    public Image heroBackground;
    public Text heroNameText;
    public Text heroHealthText;
    public Text heroLevelText;
    public Text heroAttackPowerText;
    public CanvasGroup lockedPanelCanvasGroup;

    #endregion

    private void Awake()
    {
        RefreshUI();
    }

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

    #region UI

    private void RefreshUI()
    {
        heroNameText.text = _heroName;
        heroHealthText.text = health.ToString();
        heroLevelText.text = level.ToString();
        heroAttackPowerText.text = attackPower.ToString();

        // TODO: Also set the Avatar here  later
    }

    private void ShowHeroLocked(bool islocked)
    { 
        if(islocked)

    }

    public void MainButton()
    {
        Debug.Log("HEY! I am the hero " + _heroName + ".");
    }

    #endregion
}