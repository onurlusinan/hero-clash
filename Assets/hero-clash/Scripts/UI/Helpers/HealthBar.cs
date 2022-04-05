using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    public Image healthFill;

    public void SetHealthBar(float health, float totalHealth)
    {
        healthFill.DOFillAmount(health / totalHealth, 0.2f);
    }
}
