using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleButton : MonoBehaviour
{
    private Button battleButton;

    private void Awake()
    {
        battleButton = GetComponent<Button>();
        battleButton.interactable = false;

        HeroManager.selectedHeroAmountChanged += OnSelectedHeroAmountChanged;
    }

    private void OnDestroy()
    {
        HeroManager.selectedHeroAmountChanged -= OnSelectedHeroAmountChanged;
    }

    private void OnSelectedHeroAmountChanged(int count)
    {
        bool isInteractable = (count == HeroManager.Instance.selectableHeroAmount);
        battleButton.interactable = isInteractable;
    }
}
