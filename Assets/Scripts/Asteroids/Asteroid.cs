using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private int _healtPoint;
    [SerializeField]
    private int _pricePoint;
    [SerializeField]
    private PoolManager.NamePrefab _typeAsteroid;



    [Header("Рандом кол-ва после дробления")]
    [SerializeField]
    private int _minCountSplitAsteroid;
    [SerializeField]
    private int _maxCountSplitAsteroid;

    [Header("Рандом скорости после дробления")]
    [SerializeField]
    private float _minSpeedSplitAsteroid;
    [SerializeField]
    private float _maxSpeedSplitAsteroid;



    private PoolObject _poolObject;
    private Rigidbody2D _rb;



    private void Awake()
    {
        _poolObject = gameObject.GetComponent<PoolObject>();
        _rb = GetComponent<Rigidbody2D>();

        if (_poolObject == null)
            Debug.LogError($"{this}(_poolObject): не добавлен объект PoolObject");
        if(_rb==null)
            Debug.LogError($"{this}(_rb): не добавлен объект Rigidbody2D");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Data.TAG_BULLET_PLAYER))
        {
            _healtPoint--;

            if (_healtPoint <= 0)
            {
                PlayerActor.PointsPlayer += _pricePoint;
                Destroing();
            }
        }

        if(collision.gameObject.CompareTag(Data.TAG_PLAYER) || collision.gameObject.CompareTag(Data.TAG_ENEMY))
            Destroing();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Data.TAG_MAP))
            _poolObject.ReturnToPool();
    }

    /// <summary>
    /// Активация следующего режима, псоел уничтожения астройда
    /// </summary>
    public void NextModeAsteroid()
    {
        
        switch (_typeAsteroid)
        {
            case PoolManager.NamePrefab.AsteroidBig:
                {
                    SplitAsteroid(PoolManager.NamePrefab.AsteroidMedium);
                    break;
                }
            case PoolManager.NamePrefab.AsteroidMedium:
                {
                    SplitAsteroid(PoolManager.NamePrefab.AsreroidSmall);
                    break;
                }
        }
    }

    /// <summary>
    /// Разделить астероид на колв-во в зависимости от типа
    /// </summary>
    /// <param name="needTypePrefab"></param>
    /// <param name="countPartAsteroid"></param>
    private void SplitAsteroid(PoolManager.NamePrefab needTypePrefab)
    {
        int countSplitAsteroid = UnityEngine.Random.Range(_minCountSplitAsteroid, _maxCountSplitAsteroid);
        for (int i=0; i< countSplitAsteroid; i++)
        {
            GameObject asteroidInst = PoolManager.GetObject(needTypePrefab, transform.position, transform.rotation);
            Asteroid asteroidParametrs = asteroidInst.GetComponent<Asteroid>();
            asteroidParametrs.SetSpeedAsteroid();
        }
    }

    /// <summary>
    /// Установить скорость атестройду
    /// </summary>
    /// <param name="velocity"></param>
    public void SetSpeedAsteroid(Vector2 velocity)
    {
        _rb.velocity = velocity;
    }

    /// <summary>
    /// Установить рандомную дефолтную скорость атестройда в зависимости от значений в инспеторе
    /// </summary>
    /// <param name="sppedVector"></param>
    public void SetSpeedAsteroid()
    {
        float speedX = UnityEngine.Random.Range(_minSpeedSplitAsteroid, _maxSpeedSplitAsteroid);
        float speedY = UnityEngine.Random.Range(_minSpeedSplitAsteroid, _maxSpeedSplitAsteroid);
        _rb.velocity = new Vector2(speedX, speedY);
    }

    /// <summary>
    /// Вернуть в пул
    /// </summary>
    private void Destroing()
    {
        UIManagerController.AudioSourceDestroingAsteroid.Play();
        SetSpeedAsteroid(Vector2.zero);

        NextModeAsteroid();
        _poolObject.ReturnToPool();
    }


}
