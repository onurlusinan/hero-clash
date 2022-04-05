using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using DG.Tweening;
using HeroClash.CombatSystem;
using HeroClash.HeroSystem;

namespace HeroClash.UserInterface
{
    public enum PanelType
    {
        battle,
        win,
        lose
    }

    /// <summary>
    /// Handles the Battleground UI using the BattleSystem
    /// </summary>
    public class BattleUI : MonoBehaviour
    {
        public BattleSystem battleSystem;

        [Header("BattleUI Config")]
        public Image overlay;
        public Text dialogueText;
        public Image heroBackImage;
        public Image enemyBackImage;

        [Header("Panel Switching")]
        public CanvasGroup battlePanel;
        public CanvasGroup winPanel;
        public CanvasGroup losePanel;
        private PanelType _currentPanel;

        private void Awake()
        {
            InitHeroBattleCards();

            _currentPanel = PanelType.battle;
            SwitchPanel(_currentPanel);

            overlay.DOFade(0.0f, 0.5f).OnComplete(() =>
                overlay.gameObject.SetActive(false)
            );
        }

        /// <summary>
        /// Fills the Hero battle Cards with the selected Heroes 
        /// </summary>
        private void InitHeroBattleCards()
        {
            List<int> heroIDs = HeroManager.Instance.selectedHeroes;

            for (int i = 0; i < heroIDs.Count; i++)
            {
                Hero newHero = HeroManager.Instance.GetHero(heroIDs[i]);
                battleSystem.heroBattleCards[i].InitCard(newHero);
            }
        }

        /// <summary>
        /// Prints a message to the text box
        /// </summary>
        public void PrintMessage(string message)
        {
            dialogueText.DOText(message, 0.5f, true, ScrambleMode.All);
        }

        /// <summary>
        /// Sets all battle card inputs
        /// </summary>
        public void SetAllInput(bool input)
        {
            foreach (HeroBattleCard battleHero in battleSystem.heroBattleCards)
                battleHero.SetInput(input);
        }

        /// <summary>
        /// Handles panel switching to the end game panels
        /// </summary>
        public void SwitchPanel(PanelType panelType)
        {
            switch (_currentPanel)
            {
                case PanelType.battle:
                    battlePanel.DOFade(0.0f, 0.2f);
                    battlePanel.interactable = false;
                    break;
                case PanelType.win:
                    winPanel.DOFade(0.0f, 0.2f).OnComplete(() =>
                        winPanel.gameObject.SetActive(false)
                    );
                    winPanel.interactable = false;
                    break;
                case PanelType.lose:
                    losePanel.DOFade(0.0f, 0.2f).OnComplete(() =>
                        losePanel.gameObject.SetActive(false)
                    );
                    winPanel.interactable = false;
                    break;
            }
            switch (panelType)
            {
                case PanelType.battle:
                    battlePanel.DOFade(1.0f, 0.2f);
                    battlePanel.interactable = true;
                    break;
                case PanelType.win:
                    winPanel.gameObject.SetActive(true);
                    winPanel.DOFade(1.0f, 0.2f);
                    winPanel.interactable = true;
                    break;
                case PanelType.lose:
                    losePanel.gameObject.SetActive(true);
                    losePanel.DOFade(1.0f, 0.2f);
                    losePanel.interactable = true;
                    break;
            }
            _currentPanel = panelType;
        }

        /// <summary>
        /// Method for the button in the win and lose panels
        /// Fades overlay and changes back to the hero selection scene
        /// </summary>
        public void BackToHeroSelection()
        {
            SoundManager.Instance.Play(Sounds.swoosh);
            SoundManager.Instance.DestroySources();

            overlay.gameObject.SetActive(true);
            overlay.DOFade(1.0f, 0.2f).OnComplete(() =>
                        SceneManager.LoadScene((int)SceneType.heroSelection)
            );
        }

        /// <summary>
        /// Sets the background image fades during player and enemy turns
        /// </summary>
        public void SetBackImages(bool isHeroTurn)
        {
            if (isHeroTurn)
            {
                heroBackImage.DOFade(1.0f, 0.2f);
                enemyBackImage.DOFade(0.5f, 0.2f);
            }
            else
            {
                enemyBackImage.DOFade(1.0f, 0.2f);
                heroBackImage.DOFade(0.5f, 0.2f);
            }
        }

        /// <summary>
        /// Resets image alphas at game end
        /// </summary>
        public void ResetBackImages()
        {
            heroBackImage.DOFade(1.0f, 0.2f);
            enemyBackImage.DOFade(1.0f, 0.2f);
        }
    }
}
