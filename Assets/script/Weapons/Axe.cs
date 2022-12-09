using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : BaseWeapon
{
    GameObject player;
    public float speed = 150f;


    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        float scaleX;
        scaleX = player.transform.localScale.x;
        if (scaleX < 0)
        {
            scaleX = -scaleX;
        }
        transform.localScale = new Vector3(-scaleX, 1, 1);
        transform.RotateAround(player.transform.position, new Vector3(0, 0, 1), speed * Time.deltaTime);
        //RotateByDegrees();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        Boss slaveKnight = collision.gameObject.GetComponent<Boss>();
        if (enemy != null)
        {
            enemy.Damage(damage);
        }

        if (slaveKnight != null)
        {
            slaveKnight.Damage(damage);
        }
    }

}
