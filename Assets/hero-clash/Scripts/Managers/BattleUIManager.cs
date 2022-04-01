using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class BattleUIManager : MonoBehaviour
{
    [Header("Battle-UI-Manager Config")]
    public Image overlay;

    private void Awake()
    {
        overlay.DOFade(0.0f, 0.5f).OnComplete(() =>
            overlay.gameObject.SetActive(false)
        );
    }
}
