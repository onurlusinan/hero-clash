using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour
{
    [Header("Attributes")]
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

    [Header("Panels")]
    public AttributesPanel attributesPanel;
    public abstract void RefreshCharacterCard();
}
