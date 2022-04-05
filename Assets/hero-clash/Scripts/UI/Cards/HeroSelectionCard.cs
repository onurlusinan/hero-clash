﻿using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;
using HeroClash.HeroSystem;

namespace HeroClash.UserInterface
{
    public class HeroSelectionCard : MonoBehaviour
    {
        private Hero hero;

        [Header("Images and Texts")]
        public Image characterAvatar;
        public Text characterNameText;

        [Header("Panels")]
        public GameObject lockedPanel;
        public CanvasGroup selectIndicator;
        public AttributesPanel attributesPanel;
        public RectTransform characterCard;

        public bool isSelected;

        /// <summary>
        /// Refreshes the text info of the hero card
        /// </summary>
        public void RefreshHeroCard()
        {
            characterNameText.text = hero.GetName();
            attributesPanel.RefreshPanelInfo(hero.level, hero.health, hero.attackPower);
            characterAvatar.sprite = hero.defaultAvatarSprite;
            ShowLockPanel(hero.IsLocked());
        }

        /// <summary>
        /// Initialize a hero selection card from a hero
        /// </summary>
        public void InitHeroCard(Hero hero)
        {
            this.hero = hero;
            RefreshHeroCard();
        }

        /// <summary>
        /// Button method for hero card tap
        /// </summary>
        public void HeroCardPressed()
        {
            if (hero.IsLocked())
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
            SelectionManager.Instance.SelectHero(isSelected, hero.GetID());
        }
    }
}