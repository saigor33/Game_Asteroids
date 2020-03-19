using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.CompareTag(Data.TAG_PLAYER))
            Destroing();
    }

    protected override void Destroing()
    {
        //анимация уничтожения снаряда
        base.Destroing();
    }

}
