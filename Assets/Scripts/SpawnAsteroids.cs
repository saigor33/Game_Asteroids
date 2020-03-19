using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroids : MonoBehaviour
{

    [SerializeField]
    float minX;
    [SerializeField]
    float maxX;
    [SerializeField]
    float minY;
    [SerializeField]
    float maxY;
    [SerializeField]
    float delaySpawnAsteroids;

    [SerializeField]
    float _minSpeedAsteroid;
    [SerializeField]
    float _maxSpeedAsteroid;

    [SerializeField]
    int _minCountCreateAsteroidsInTime;
    [SerializeField]
    int _maxCountCreateAsteroidsInTime;



    private void Start()
    {
        InvokeRepeating(nameof(CreateAsteroids), 0, delaySpawnAsteroids);
    }

    private void CreateAsteroids()
    {
       
        int countAsteroid = UnityEngine.Random.Range(_minCountCreateAsteroidsInTime, _maxCountCreateAsteroidsInTime);
        for (int i=0; i< countAsteroid; i++)
        {
            int direction = UnityEngine.Random.Range(0, 4);

            GameObject asteroidInst = PoolManager.GetObject(PoolManager.NamePrefab.AsteroidBig, GetStartPositionAsteroid(direction), Quaternion.identity);
            //GameObject asteroidInst = Instantiate(_asteroidBig,GetStartPositionAsteroid(direction), Quaternion.identity, transform);
            Asteroid asteroidParametrs = asteroidInst.GetComponent<Asteroid>();
            asteroidParametrs.SetSpeedAsteroid(GetStartSpeedAsteroid(direction));
        }
    }

    /// <summary>
    /// Получить рандомную точку появления астеройда в зависимости от стороны появления
    /// 0 - лево, 1 - верх, 2 - низ, 3 - право
    /// </summary>
    /// <param name="direction">Номер стороны появления</param>
    /// <returns></returns>
    private Vector2 GetStartPositionAsteroid(int direction)
    {

        float posX = 0;
        float posY = 0;

        switch (direction)
        {
            //лево
            case 0: return new Vector2(minX, UnityEngine.Random.Range(minY, maxY));
            // верх
            case 1: return new Vector2(UnityEngine.Random.Range(minX, maxX), maxY);
            //низ
            case 2: return new Vector2(UnityEngine.Random.Range(minX, maxX), minY);
            // право
            case 3: return new Vector2(maxX, UnityEngine.Random.Range(minY, maxY));
        }
        Debug.LogError($"В метод GetStartPositionAsteroid() передано не правильное направление. Значение = {direction}");
        return new Vector2(posX, posY);
    }

    /// <summary>
    /// Получить рандомную скорость  астеройда в зависимости от стороны появления
    /// 0 - появился слева, 1 - верх, 2 - низ, 3 - право
    /// </summary>
    /// <param name="direction">Номер стороны появления</param>
    /// <returns></returns>
    private Vector2 GetStartSpeedAsteroid(int direction)
    {
        float speedX = UnityEngine.Random.Range(_minSpeedAsteroid , _maxSpeedAsteroid);
        float speedY = UnityEngine.Random.Range(_minSpeedAsteroid, _maxSpeedAsteroid);

        switch (direction)
        {
            //лево
            case 0: return new Vector2(speedX, speedY);
            // верх
            case 1: return new Vector2(speedX, -speedY);
            // низ
            case 2: return new Vector2(speedX, speedY);
            // право
            case 3: return new Vector2(-speedX, speedY);
        }

        Debug.LogError($"В метод GetStartSpeedAsteroid() передано не правильное направление. Значение = {direction}");
        return new Vector2(speedX, speedY);
    }
}
