using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class DamageDisplay : MonoBehaviour
{
    [Header("UI-Config")]
    public Text damageText;
    private CanvasGroup displayCanvasGroup;

    private void Awake()
    {
        displayCanvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowText(InteractionType interactionType, float amount)
    {
        float originalPosY = transform.position.y;

        switch (interactionType)
        {
            case InteractionType.attack:
                damageText.text = amount + "ap!";
                damageText.color = Color.green;
                break;
            case InteractionType.damage:
                damageText.text = "-" + amount + "hp!";
                damageText.color = Color.red;
                break;
        }

        Sequence _displaySequence = DOTween.Sequence();
        _displaySequence.Append(displayCanvasGroup.DOFade(1.0f, 0.1f))
                        .Append(transform.DOScale(1.2f, 1.5f))
                        .Append(displayCanvasGroup.DOFade(0.0f, 0.1f))
                        .OnComplete(() =>
                            transform.DOScale(1.0f, 0.0f)
                        );
    }
}