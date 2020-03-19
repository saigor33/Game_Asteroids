using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{

    protected Rigidbody2D _rb;
    protected PoolObject _poolObject;


    protected virtual void Awake()
    {
        _poolObject = gameObject.GetComponent<PoolObject>();
        _rb = GetComponent<Rigidbody2D>();
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Data.TAG_ASTEROID))
            Destroing();

        
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Data.TAG_MAP))
            _poolObject.ReturnToPool();
    }

    /// <summary>
    /// Разрушение снаряда 
    /// </summary>
    protected virtual void Destroing()
    {
        _poolObject.ReturnToPool();
    }


    /// <summary>
    /// Установить скорость снаряду
    /// </summary>
    /// <param name="velocity"></param>
    public virtual void SetSpeedBullet(Vector2 velocity)
    {
        _rb.velocity = velocity;
    }
}
