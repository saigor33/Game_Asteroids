using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerActor : Actor
{
    private static int _countSourcePoint;

    /// <summary>
    /// Очки игрока, полученные за уничтожение астеройдов и противников
    /// </summary>
    public static int PointsPlayer
    {
        get { return _countSourcePoint; }
        set
        {
            _countSourcePoint = value;
            _eventUpdateDataSource.Invoke();
        }
    } 
    private static UnityEvent _eventUpdateDataSource;

    [SerializeField]
    private UIManagerController _uiManagerController;
    [SerializeField]
    private float _speedShip;
    [SerializeField]
    private float _speedRotation;
    [SerializeField]
    private float _delayRiseAgain;
    [SerializeField]
    private AudioSource _audioSourceExplosion;


    private bool _statusCanGetDamage;
    private SpriteRenderer _spiteRendereCurrentObject;

    protected override void Awake()
    {
        base.Awake();
        _statusCanGetDamage = true;
        _eventUpdateDataSource = new UnityEvent();
        _eventUpdateDataSource.AddListener(UpdateSourceOnUI);
        _spiteRendereCurrentObject = GetComponent<SpriteRenderer>();

        if (_uiManagerController ==null)
            Debug.LogError($"{this}(_uiManagerController): не добавлен объект UIManagerController");
        if (_audioSourceExplosion == null)
            Debug.LogError($"{this}(_audioSourceExplosion): не добавлен объект AudioSource");

        _countSourcePoint = 0;
        UpdateHealtPointOnUI();
    }

    void Update()
    {
        if (!Input.anyKey || Time.timeScale == 0)
            return;

        if (Input.GetButtonDown("FireUsual"))
            Shoot();
    }


    private void FixedUpdate()
    {
        if (!Input.anyKey || Time.timeScale == 0)
            return;

        MovementLogic();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Data.TAG_ASTEROID))
            Destroing();

        if (collision.gameObject.CompareTag(Data.TAG_ENEMY))
            Destroing();
    }

    /// <summary>
    /// Обновить значение количества набранных очков
    /// </summary>
    private void UpdateSourceOnUI()
    {
        _uiManagerController.UpdateTextForSourcePoints(PointsPlayer.ToString());
    }

    /// <summary>
    /// Обноавить значение очков здоровья на UI
    /// </summary>
    private void UpdateHealtPointOnUI()
    {
        _uiManagerController.UpdateTextForHealthPoint(_healthPoint.ToString());
    }

    /// <summary>
    /// Очки здоровья
    /// </summary>
    private int HealtPoint 
    { 
        get { return _healthPoint; } 
        set 
        { 
            _healthPoint = value;
            UpdateHealtPointOnUI();
        }
    }

    protected override void Destroing()
    {
        if (_statusCanGetDamage)
        {
            HealtPoint--;
            //добавить анимацию разрушения 
            transform.position = Vector3.zero;
            _audioSourceExplosion.Play();
            StartCoroutine(RiseAgain());
            CheckHealtPoint();
        }

    }

    /// <summary>
    /// Выстрел снарядом
    /// </summary>
    protected override void Shoot()
    {
        GameObject bulletIns = PoolManager.GetObject(PoolManager.NamePrefab.BulletPlayer, _weapon.transform.position, transform.rotation);
        Bullet bullet = bulletIns.GetComponent<Bullet>();
        _audioSourceFire.Play();
        bullet.SetSpeedBullet(gameObject.transform.up * _speedBullet);
    }

    /// <summary>
    /// Обработка движений корабля
    /// </summary>
    private void MovementLogic()
    {
        float moveHorizontal = Input.GetAxis(Data.CONTROL_MOVE_HORIZONTAL);
        float moveVertical = Input.GetAxis(Data.CONTROL_MOVE_VERTICAL);

        Vector2 movement = new Vector3(moveHorizontal, moveVertical);

        _rb.AddRelativeForce(movement * _speedShip);
        gameObject.transform.Rotate(0, 0, moveHorizontal * _speedRotation);
    }



    /// <summary>
    /// Проверить на проигрышь
    /// </summary>
    protected override void CheckHealtPoint()
    {
        if (_healthPoint <= 0)
            _uiManagerController.GameOver();

    }

    /// <summary>
    /// Воскрешение игрока  и запуск метода мерцания после смерти
    /// </summary>
    /// <returns></returns>
    private IEnumerator RiseAgain()
    {
        _statusCanGetDamage = false;
        InvokeRepeating(nameof(Flicker), 0, 0.2f);
        yield return new WaitForSeconds(_delayRiseAgain);

        CancelInvoke(nameof(Flicker));
        _spiteRendereCurrentObject.enabled = true; 
        _statusCanGetDamage = true;
    }

    /// <summary>
    /// Мирцание корабля после смерти
    /// </summary>
    private void Flicker()
    {
        _spiteRendereCurrentObject.enabled = !_spiteRendereCurrentObject.enabled;
    }

    private void OnDestroy()
    {
        _eventUpdateDataSource.RemoveAllListeners();
    }
}
