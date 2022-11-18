using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : BaseWeapon
{
    [SerializeField] Player player;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        transform.Rotate(0, 0, 50 * level * Time.deltaTime);
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
}
