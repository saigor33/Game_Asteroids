using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Data.TAG_ASTEROID))
            collision.gameObject.GetComponent<PoolObject>().ReturnToPool();

        if (collision.gameObject.tag.Contains(Data.TAG_BULLET))
            collision.gameObject.GetComponent<PoolObject>().ReturnToPool();

        if(collision.gameObject.CompareTag(Data.TAG_ENEMY))
            collision.gameObject.GetComponent<PoolObject>().ReturnToPool();
    }
}
