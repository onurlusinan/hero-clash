using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class Hero : Character
{
    #region VARIABLES & REFS
    [SerializeField] private int _heroID;

    [Header("Hero Experience-Related")]
    public int level;
    public bool isSelected;

    private int _experience;
    [SerializeField]private bool _isLocked;

    [Header("Hero-Spesific Panels")]
    public CanvasGroup lockedPanel;
    public CanvasGroup selectIndicator;
    public RectTransform characterCard;

    #endregion

    private void Awake()
    {
        isSelected = false;
    }

    #region GETTERS - SETTERS
    public int GetID() => _heroID;
    public string GetName() => characterName;
    public int GetExperience() => _experience;
    public bool IsLocked() => _isLocked;
    public void SetID(int id) => _heroID = id;
    public void SetHeroName(string name) => characterName = name; 
    public void SetExperience(int experience) => _experience = experience;
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
        HeroSaveData data = SaveSystem.LoadHero(this);

        SetID(data.id);
        SetHeroName(data.heroName);
        SetExperience(data.experience);
        health = data.health;
        attackPower = data.attackPower;
        SetLock(data.isLocked);

        RefreshCharacterCard();
    }

    public void LoadBaseData(HeroBaseData baseData)
    {
        SetID(baseData.GetID());
        SetHeroName(baseData.GetHeroName());

        SetExperience(baseData.GetExperience());
        level = CalculateLevel(_experience);

        health = baseData.GetHealth();
        attackPower = baseData.GetAttackPower();

        _isLocked = baseData.IsLocked();

        defaultAvatarSprite = baseData.GetDefaultAvatar();
    }

    private int CalculateLevel(int experience)
    {
        return experience / HeroManager.Instance.experiencePerLevel;
    }

    #endregion

    #region UI

    /// <summary>
    /// Refreshes the text info of the hero card
    /// </summary>
    public override void RefreshCharacterCard()
    {
        characterNameText.text = characterName;
        attributePanel.RefreshPanelInfo(level.ToString(), health.ToString(), attackPower.ToString());
        characterAvatar.sprite = defaultAvatarSprite;

        SetLock(_isLocked);
    }

    /// <summary>
    /// OverShows/hides the Attributes Panel pop-up
    /// </summary>
    public override void ShowAttributesPanel(bool show)
    {
        if (_isLocked)
            return;

        attributePanel.ShowPanel(show);
    }

    /// <summary>
    /// Override method for selecting on the hero card
    /// </summary>
    public override void CharacterPressed()
    {
        if (_isLocked)
            return;

        Select(!isSelected);
    }

    /// <summary>
    /// Shows/hides the lock panel
    /// </summary>
    private void ShowLockPanel(bool islocked)
    {
        if (islocked)
        {
            lockedPanel.gameObject.SetActive(true);
            lockedPanel.DOFade(1.0f, 0.2f);
        }
        else
        {
            Debug.Log("Setting " + characterName + " lock to " + islocked);
            lockedPanel.DOFade(0.0f, 0.2f).OnComplete(() =>
                lockedPanel.gameObject.SetActive(false)
            );
        }
    }

    /// <summary>
    /// shows/hides the SELECTED text behind the character cards
    /// </summary>
    /// <param name="show"></param>
    private void ShowSelectIndicator(bool show)
    {
        if (show)
            selectIndicator.DOFade(1.0f, 0.2f);
        else
            selectIndicator.DOFade(0.0f, 0.2f);
    }

    /// <summary>
    /// Moves the hero card down when selected
    /// </summary>
    private void MoveHeroCard(bool selected)
    {
        ShowSelectIndicator(selected);

        if (selected)
            characterCard.DOAnchorPosY(-60f, 0.2f);
        else
            characterCard.DOAnchorPosY(0f, 0.2f);
    }

    #endregion

    #region SELECTING
    public void Select(bool select)
    {
        isSelected = select;

        MoveHeroCard(isSelected);

        HeroManager.Instance.SelectHero(isSelected, _heroID);
    }
    #endregion
}