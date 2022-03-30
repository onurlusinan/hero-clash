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

    [Header("Hero Sprites")]
    public Sprite defaultAvatarSprite;
    public Sprite attackingAvatarSprite;
    public Sprite damagedAvatarSprite;

    [Header("Images and Texts")]
    public Image heroAvatar;
    public Image heroBackground;
    public Text heroNameText;
    public Text heroHealthText;
    public Text heroLevelText;
    public Text heroAttackPowerText;

    [Header("Panels")]
    public CanvasGroup lockedPanel;
    public CanvasGroup attributePanel;
    public CanvasGroup checkboxPanel;

    #endregion

    private void Awake()
    {
        RefreshInfo();
        ShowLockPanel(_isLocked);
    }

    #region GETTERS - SETTERS
    public int GetID() => _heroID;
    public string GetName() => characterName;
    public float GetExperience() => _experience;
    public bool IsLocked() => _isLocked;
    public void SetID(int id) => _heroID = id;
    public void SetHeroName(string name) => characterName = name; 
    public void SetExperience(float experience) => _experience = experience;
    public void SetLock(bool locked) 
    { 
        _isLocked = locked;
        ShowLockPanel(locked);
    } 
    #endregion

    #region SAVE-SYSTEM

    /// <summary>
    /// Saves the hero
    /// </summary>
    public void SaveHero()
    {
        SaveSystem.SaveHero(this);
    }

    /// <summary>
    /// Loads the hero
    /// </summary>
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

    /// <summary>
    /// Refreshes the text info of the hero card
    /// </summary>
    private void RefreshInfo()
    {
        heroNameText.text = characterName;
        heroHealthText.text = health.ToString();
        heroLevelText.text = level.ToString();
        heroAttackPowerText.text = attackPower.ToString();
    }

    /// <summary>
    /// Shows/hides the lock and the checkbox panels
    /// </summary>
    private void ShowLockPanel(bool islocked)
    {
        if (islocked)
        {
            lockedPanel.gameObject.SetActive(true);
            lockedPanel.DOFade(1.0f, 0.2f);

            checkboxPanel.DOFade(0.0f, 0.2f).OnComplete(() =>
                checkboxPanel.gameObject.SetActive(false)
            );
        }
        else
        {
            checkboxPanel.gameObject.SetActive(true);
            checkboxPanel.DOFade(1.0f, 0.2f);

            lockedPanel.DOFade(0.0f, 0.2f).OnComplete(() =>
                lockedPanel.gameObject.SetActive(false)
            );
        }
            
    }

    /// <summary>
    /// Shows/hides the Attributes Panel pop-up
    /// </summary>
    public void ShowAttributesPanel(bool show)
    {
        if (_isLocked)
            return;

        if (show)
            attributePanel.DOFade(1.0f, 0.2f);
        else
            attributePanel.DOFade(0.0f, 0.2f);
    }

    /// <summary>
    /// Method for the main button on the hero card
    /// </summary>
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