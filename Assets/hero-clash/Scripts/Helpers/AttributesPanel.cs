using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public void RefreshPanelInfo(string level, string health, string attackPower)
    {
        levelText.text = level;
        healthText.text = health;
        attackPowerText.text = attackPower;
    }

    public void ShowPanel(bool show)
    {
        if (show)
            canvasGroup.DOFade(1.0f, 0.2f);
        else
            canvasGroup.DOFade(0.0f, 0.2f);
    }
}
