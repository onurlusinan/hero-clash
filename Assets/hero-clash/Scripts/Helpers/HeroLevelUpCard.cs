using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroLevelUpCard : MonoBehaviour
{
    public Text heroName;

    public void SetCard(Hero hero)
    {
        heroName.text = hero.GetName();
    }
}
