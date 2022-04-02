using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private EnemyBaseDataCollection _enemyBaseDataCollection;
    public Enemy enemy;

    private void Awake()
    {
        _enemyBaseDataCollection = Resources.Load<EnemyBaseDataCollection>("EnemyBaseData/EnemyBaseDataCollection");

        InstantiateEnemy();
    }

    private void InstantiateEnemy()
    {
        EnemyBaseData newEnemyBaseData = _enemyBaseDataCollection.GetRandomEnemy();
        enemy.LoadBaseData(newEnemyBaseData);
    }


}
