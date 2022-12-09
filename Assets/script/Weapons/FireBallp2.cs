using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallp2 : BaseWeapon
{
    void Update()
    {
        //transform.Rotate(0, 0, 12);
        transform.position -= transform.right * 5 * Time.deltaTime; ;
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
        //Destroy(gameObject);

    }

}
