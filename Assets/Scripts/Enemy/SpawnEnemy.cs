using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : Spawner
{
    protected override void CreateObjectSpawn()
    {
        int countEnemy = UnityEngine.Random.Range(_minCountCreateInTime, _maxCountCreateInTime);
        for (int i = 0; i < countEnemy; i++)
        {
            //враги появляются только с краёв карты
            int direction = UnityEngine.Random.Range(0, 4) > 2 ? 3 : 0;

            GameObject enemyInst = PoolManager.GetObject(PoolManager.NamePrefab.EnemyPlayer, GetStartPositionSpanObject(direction), Quaternion.identity);
            EnemyPlayer enemyParametrs = enemyInst.GetComponent<EnemyPlayer>();
            enemyParametrs.SetSpeed(GetStartSpeedSpawnOnject(direction));
            enemyParametrs.StartShoot();
        }
    }



}
