using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayer : Actor
{
    [SerializeField]
    protected int _pricePoint;

    protected PoolObject _poolObject;

    protected override void Awake()
    {
        base.Awake();
        _poolObject = gameObject.GetComponent<PoolObject>();
        if (_poolObject == null)
            Debug.LogError($"{this}(_poolObject): не добавлен объект PoolObject");
    }

    private void Start()
    {
        StartShoot();
    }

    /// <summary>
    /// Начать стрельбу
    /// </summary>
    public virtual void StartShoot()
    {
        InvokeRepeating(nameof(Shoot), 0, _cooldownShoot);
    }

    /// <summary>
    /// Выстрел снарядом 
    /// </summary>
    protected override void Shoot()
    {
        GameObject bulletEnemy = PoolManager.GetObject(PoolManager.NamePrefab.EnemyBullet, _weapon.transform.position, Quaternion.identity);
        EnemyBullet bulletScript = bulletEnemy.GetComponent<EnemyBullet>();
        _audioSourceFire.Play();
        bulletScript.SetSpeedBullet(-gameObject.transform.up * _speedBullet);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag(Data.TAG_BULLET_PLAYER))
        {
            collision.gameObject.GetComponent<PoolObject>().ReturnToPool();
            _healthPoint--;
            PlayerActor.PointsPlayer += _pricePoint;

            if (_healthPoint <= 0)
                Destroing();
        }

        if (collision.gameObject.CompareTag(Data.TAG_PLAYER) || collision.gameObject.CompareTag(Data.TAG_ASTEROID))
            Destroing();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Data.TAG_MAP))
            _poolObject.ReturnToPool();
    }

    /// <summary>
    /// Разрушение актора при смерти
    /// </summary>
    protected override void Destroing()
    {
        //анимация взырва
        CancelInvoke();
        _poolObject.ReturnToPool();
    }

    /// <summary>
    /// Текущие жизни актора
    /// </summary>
    public int HealtPoint
    {
        get { return _healthPoint; }
        set
        {
            _healthPoint = value;
            CheckHealtPoint();
        }
    }
}
