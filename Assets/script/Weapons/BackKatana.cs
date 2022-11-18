using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackKatana : BaseWeapon
{
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        StartCoroutine(KatanaCoroutine());
    }

    IEnumerator KatanaCoroutine()
    {
        while (true)
        {
            boxCollider.enabled = false;
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.5f - level / 100);


            boxCollider.enabled = true;
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.5f);
        }

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
