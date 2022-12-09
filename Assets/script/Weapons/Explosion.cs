using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : BaseWeapon
{
    void Start()
    {
        StartCoroutine(AutoDestruction());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Damage(damage);
        }
        Boss boss = collision.gameObject.GetComponent<Boss>();
        if (boss != null)
        {
            boss.Damage(damage);
        }
    }

    IEnumerator AutoDestruction()
    {
        yield return new WaitForSeconds(1.75f);
        Destroy(gameObject);

    }

}
