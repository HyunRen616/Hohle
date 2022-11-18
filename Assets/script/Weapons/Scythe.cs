using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class Scythe : BaseWeapon
{
    void Update()
    {
        //transform.Rotate(0, 0, 12);
        transform.position += transform.up * 5 * Time.deltaTime;;
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
