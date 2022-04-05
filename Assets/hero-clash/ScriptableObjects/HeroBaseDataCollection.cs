using UnityEngine;

/// <summary>
/// Collection of hero base data
/// </summary>
[CreateAssetMenu(fileName = "HeroBaseDataCollection", menuName = "ScriptableObjects/HeroBaseDataCollection")]
public class HeroBaseDataCollection : ScriptableObject
{
    [SerializeField]
    private HeroBaseData[] heroBaseDatas;

    public HeroBaseData[] GetCollection()
    {
        return this.heroBaseDatas;
    }
}
