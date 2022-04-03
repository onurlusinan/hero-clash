using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBaseDataCollection", menuName = "ScriptableObjects/EnemyBaseDataCollection")]
public class EnemyBaseDataCollection : ScriptableObject
{
    [SerializeField]
    private EnemyBaseData[] enemyBaseDatas;

    public EnemyBaseData GetRandomEnemy()
    {
        int randomIndex = Random.Range(0, enemyBaseDatas.Length);
        return enemyBaseDatas[randomIndex];
    }
}
