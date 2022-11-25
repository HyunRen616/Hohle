using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallp2 : BaseWeapon
{
    GameObject player;
    SpriteRenderer spriteRenderer;
    CircleCollider2D circleCollider;
    int a = 0;
    float direction = 0;
    [SerializeField] float speed = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        StartCoroutine(StandardEnergyBallCoroutine());
    }

    IEnumerator StandardEnergyBallCoroutine()
    {
        circleCollider.enabled = true;
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(5f);


        circleCollider.enabled = false;
        spriteRenderer.enabled = false;
        Destroy(gameObject);

    }



    // Update is called once per frame
    void Update()
    {

        if (player != null)
        {
            float scaleX;
            scaleX = player.transform.localScale.x;
            if (a < 1)
            {
                a = 1;
                direction = -scaleX;
            }

            transform.position += new Vector3(direction, 0, 0) * Time.deltaTime * speed;
            transform.localScale = new Vector3(direction, 1, 1);
        }

        else
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Damage(1);
        }

    }
}
