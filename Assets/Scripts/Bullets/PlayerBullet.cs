using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.CompareTag(Data.TAG_ENEMY))
        {
            EnemyPlayer enemy = collision.gameObject.GetComponent<EnemyPlayer>();
            enemy.HealtPoint--;
            Destroing();
        }
    }


    protected override void Destroing()
    {
        //добавить анимацию уничтожения снаряда
        base.Destroing();
    }

}
