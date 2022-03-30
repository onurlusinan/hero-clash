using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("Character Attributes")]
    public string characterName;
    public float health;
    public float attackPower;
}
