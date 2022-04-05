using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

namespace HeroClash.UserInterface
{
    public class AttributesPanel : MonoBehaviour
    {
        [Header("Panel UI Config")]
        public Text levelText;
        public Text healthText;
        public Text attackPowerText;

        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        public void RefreshPanelInfo(int level, float health, float attackPower)
        {
            if (levelText != null)
                levelText.text = level.ToString();

            healthText.text = health.ToString();
            attackPowerText.text = attackPower.ToString();
        }

        public void RefreshPanelInfo(float health, float attackPower)
        {
            healthText.text = health.ToString();
            attackPowerText.text = attackPower.ToString();
        }

        public void ShowPanel(bool show)
        {
            if (show)
                canvasGroup.DOFade(1.0f, 0.1f);
            else
                canvasGroup.DOFade(0.0f, 0.1f);
        }
    }
}