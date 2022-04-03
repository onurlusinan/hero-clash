using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class Hero : MonoBehaviour
{
    #region VARIABLES & REFS

    [Header("Attributes")]
    [SerializeField] private int _heroID;
    public string characterName;
    public float health;
    public float attackPower;

    [Header("Sprites")]
    public Sprite defaultAvatarSprite;
    public Sprite attackingAvatarSprite;
    public Sprite damagedAvatarSprite;

    [Header("Images and Texts")]
    public Image characterAvatar;
    public Image characterBackground;
    public Text characterNameText;

    [Header("Hero Experience-Related")]
    public int level;
    public bool isSelected;
    [SerializeField]private int _experience;
    private bool _isLocked;

    [Header("Panels")]
    public GameObject lockedPanel;
    public CanvasGroup selectIndicator;
    public AttributesPanel attributesPanel;
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
    }
    #endregion

    #region LEVELING

    public void LevelUp()
    {
        _experience++;
        level = _experience / HeroManager.Instance.experiencePerLevel;
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
        level = CalculateLevel(_experience);
        health = (data.health) * Mathf.Pow(1.1f, level);
        attackPower = (data.attackPower) * Mathf.Pow(1.1f, level);
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
    public void RefreshCharacterCard()
    {
        characterNameText.text = characterName;
        attributesPanel.RefreshPanelInfo(level, health, attackPower);
        characterAvatar.sprite = defaultAvatarSprite;

        SetLock(_isLocked);
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