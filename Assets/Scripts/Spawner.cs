using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField]
    protected float _minX;
    [SerializeField]
    protected float _maxX;
    [SerializeField]
    protected float _minY;
    [SerializeField]
    protected float _maxY;
    [SerializeField]
    protected float _delaySpawn;

    [SerializeField]
    protected float _minSpeedX;
    [SerializeField]
    protected float _maxSpeedX;
    [SerializeField]
    protected float _minSpeedY;
    [SerializeField]
    protected float _maxSpeedY;


    [SerializeField]
    protected int _minCountCreateInTime;
    [SerializeField]
    protected int _maxCountCreateInTime;

    protected virtual void Start()
    {
        InvokeRepeating(nameof(CreateObjectSpawn), 0, _delaySpawn);
    }


    protected abstract void CreateObjectSpawn();


    /// <summary>
    /// Получить рандомную точку появления астеройда в зависимости от стороны появления
    /// 0 - лево, 1 - верх, 2 - низ, 3 - право
    /// </summary>
    /// <param name="direction">Номер стороны появления</param>
    /// <returns></returns>
    protected Vector2 GetStartPositionSpanObject(int direction)
    {

        float posX = 0;
        float posY = 0;

        switch (direction)
        {
            //лево
            case 0: return new Vector2(_minX, UnityEngine.Random.Range(_minY, _maxY));
            // верх
            case 1: return new Vector2(UnityEngine.Random.Range(_minX, _maxX), _maxY);
            //низ
            case 2: return new Vector2(UnityEngine.Random.Range(_minX, _maxX), _minY);
            // право
            case 3: return new Vector2(_maxX, UnityEngine.Random.Range(_minY, _maxY));
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
    protected Vector2 GetStartSpeedSpawnOnject(int direction)
    {
        float speedX = UnityEngine.Random.Range(_minSpeedX, _maxSpeedX);
        float speedY = UnityEngine.Random.Range(_minSpeedY, _maxSpeedY);

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
