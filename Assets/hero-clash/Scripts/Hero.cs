using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class Hero : Character
{
    #region ATTRIBUTES
    [SerializeField] private int _heroID;

    [Header("Experience-Related")]
    public float _experience;
    public int level;
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
        ShowHeroLocked(_isLocked);
    }

    #region GETTERS - SETTERS
    public int GetID() => _heroID;
    public string GetName() => characterName;
    public float GetExperience() => _experience;
    public void SetID(int id) => _heroID = id;
    public void SetHeroName(string name) => characterName = name; 
    public void SetExperience(float experience) => _experience = experience;
    public bool IsLocked() => _isLocked;
    public void SetLock(bool locked) 
    { 
        _isLocked = locked;
        ShowHeroLocked(locked);
    } 
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
        SetLock(data.isLocked);
    }

    #endregion

    #region UI

    private void RefreshUI()
    {
        heroNameText.text = characterName;
        heroHealthText.text = health.ToString();
        heroLevelText.text = level.ToString();
        heroAttackPowerText.text = attackPower.ToString();
    }

    private void ShowHeroLocked(bool islocked)
    {
        if (islocked)
        {
            lockedPanelCanvasGroup.gameObject.SetActive(true);
            lockedPanelCanvasGroup.DOFade(1.0f, 0.2f);
        }
        else
            lockedPanelCanvasGroup.DOFade(0.0f, 0.2f).OnComplete(() =>
                lockedPanelCanvasGroup.gameObject.SetActive(false)
            );
    }

    public void MainButton()
    {
        if (_isLocked)
            Debug.Log("Hero locked!");
        else
        { 
            Debug.Log("HEY! I am the hero " + characterName + ".");
        }
    }

    #endregion
}