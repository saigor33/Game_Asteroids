using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    [SerializeField]
    protected int _healthPoint;
    [SerializeField]
    protected float _cooldownShoot;
    [SerializeField]
    protected float _speedBullet;

    [SerializeField]
    protected GameObject _weapon;
    [SerializeField]
    protected AudioSource _audioSourceFire;

    protected Rigidbody2D _rb;


    protected abstract void Destroing();
    protected abstract void Shoot();
    protected virtual void CheckHealtPoint()
    {
        if (_healthPoint <= 0)
            Destroing();
    }


    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null)
            Debug.LogError($"{this}(_rb): не добавлен объект Rigidbody2D");
        if (_audioSourceFire == null)
            Debug.LogError($"{this}(_audioSourceFire): не добавлен объект AudioSource");
        if (_weapon == null)
            Debug.LogError($"{this}(_weapon): не добавлен объект _weapon");
    }

    /// <summary>
    /// Установить скорость актору
    /// </summary>
    /// <param name="velocity"></param>
    public virtual void SetSpeed(Vector2 velocity)
    {
        _rb.velocity = velocity;
    }





}
