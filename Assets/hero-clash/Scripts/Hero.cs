using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

/// <summary>
/// The main hero class
/// </summary>
public class Hero : MonoBehaviour
{
    #region VARIABLES & REFS

    [Header("Attributes")]
    [SerializeField] private int _heroID;
    [SerializeField] private string characterName;
    public float health;
    public float attackPower;

    [Header("Hero Experience-Related")]
    public int level;
    [SerializeField]private int _experience;
    [SerializeField]private bool _isLocked;

    [Header("Sprites")]
    internal Sprite defaultAvatarSprite;

    [Header("Images and Texts")]
    public Image characterAvatar;
    public Image characterBackground;
    public Text characterNameText;
    
    [Header("Panels")]
    public GameObject lockedPanel;
    public CanvasGroup selectIndicator;
    public AttributesPanel attributesPanel;
    public RectTransform characterCard;

    public bool isSelected;
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
    public void SetExperience(int experience)
    { 
        _experience = experience;
        level = CalculateLevel(_experience);
    } 
    public void SetLock(bool locked) => _isLocked = locked;
    
    #endregion

    #region LEVELING

    public void LevelUp()
    {
        _experience = _experience + 1;

        int previousLevel = level;
        int newLevel = CalculateLevel(_experience);

        if (newLevel > previousLevel)
        {
            health = health * 1.1f;
            attackPower = attackPower * 1.1f;
            SaveHero();

            Debug.Log(characterName + " has leveled up! " + previousLevel + " -> " + newLevel);
        }

        level = newLevel;
    }

    private int CalculateLevel(int experience)
    {
        level = experience / HeroManager.Instance.experiencePerLevel;
        return level + 1;
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

        if (data == null)
            return;

        SetID(data.id);
        SetHeroName(data.heroName);
        SetExperience(data.experience);
        health = (data.health);
        attackPower = (data.attackPower);
        SetLock(data.isLocked);

        RefreshHeroCard();
    }

    public void LoadBaseData(HeroBaseData baseData)
    {
        SetID(baseData.GetID());
        SetHeroName(baseData.GetHeroName());

        health = baseData.GetHealth();
        attackPower = baseData.GetAttackPower();
        SetExperience(0);

        defaultAvatarSprite = baseData.GetDefaultAvatar();
        

        RefreshHeroCard();
    }

    #endregion

    #region UI

    /// <summary>
    /// Refreshes the text info of the hero card
    /// </summary>
    public void RefreshHeroCard()
    {
        characterNameText.text = characterName;
        attributesPanel.RefreshPanelInfo(level, health, attackPower);
        characterAvatar.sprite = defaultAvatarSprite;
        ShowLockPanel(_isLocked);
    }

    /// <summary>
    /// Override method for selecting on the hero card
    /// </summary>
    public void CharacterPressed()
    {
        if (_isLocked)
            return;

        Select(!isSelected);
    }

    /// <summary>
    /// Shows/hides the lock panel
    /// </summary>
    private void ShowLockPanel(bool isLocked)
    {
        lockedPanel.SetActive(isLocked);
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

    public void Select(bool select)
    {
        isSelected = select;

        MoveHeroCard(isSelected);

        HeroManager.Instance.SelectHero(isSelected, _heroID);
    }
    #endregion
}