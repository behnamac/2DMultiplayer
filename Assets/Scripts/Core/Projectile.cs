using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float destroyTimer = 3f;

    private void Start()
    {
        Invoke(nameof(destroyItself), destroyTimer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            destroyItself();
        }
    }

    private void destroyItself()
    {
        Destroy(gameObject);
    }
}
