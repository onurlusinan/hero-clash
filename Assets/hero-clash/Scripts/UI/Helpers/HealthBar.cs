using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

namespace HeroClash.UserInterface
{
    public class HealthBar : MonoBehaviour
    {
        public Image healthFill;

        public void SetHealthBar(float health, float totalHealth)
        {
            healthFill.DOFillAmount(health / totalHealth, 0.2f);
        }
    }
}