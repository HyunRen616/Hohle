using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    //GameObject player;
    //[SerializeField] GameObject crystal;
    //[SerializeField] GameObject magnet;
    //[SerializeField] GameObject orb;
    //[SerializeField] GameObject font;
    //[SerializeField] float speed = 1f;
    public int bossHP = 1;
    //public int maxHp = 1;

    Material material;

    SpriteRenderer spriteRenderer;

    [SerializeField] GameObject fireBall;

    Animator animator;
    enum BossState
    {
        regular = 0,
        moving = 1,
        Attacking = 2
    }

    BossState bossState = BossState.regular;

    float waitTimer = 1f;

    // Start is called before the first frame update
    protected override void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        base.Start();
        material = spriteRenderer.material;
        //StartCoroutine(BossCameraCoroutine());
    }

    IEnumerator BossCameraCoroutine()
    {
        //Time.timeScale = 0;

        //Camera.main.GetComponent<PlayerCamera>().target = transform;
        //Camera.main.orthographicSize = 4;
        float bossX = transform.position.x;
        float bossY = transform.position.y;
        float cameraZ = transform.position.z;
        var targetPosition = new Vector3(bossX, bossY, cameraZ);
        Camera.main.GetComponent<PlayerCamera>().transform.position = targetPosition;
        yield return new WaitForSecondsRealtime(5f);

        //Camera.main.GetComponent<PlayerCamera>().target = player.transform;
        //Camera.main.orthographicSize = 4;

        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;
        float cameraZ2 = transform.position.z;
        var targetPosition2 = new Vector3(playerX, playerY, cameraZ2);
        Camera.main.GetComponent<PlayerCamera>().transform.position = targetPosition2;
        yield return new WaitForSecondsRealtime(2f);

        Time.timeScale = 1;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (player != null)
        {
            float ratio = (float)bossHP / maxHp;
            material.SetFloat("_Anger", 1 - ratio);
            switch (bossState)
            {
                case BossState.regular:
                    waitTimer -= Time.deltaTime;
                    if (waitTimer <= 0)
                    {
                        bossState = BossState.moving;
                    }
                    break;
                case BossState.moving:
                    base.Update();
                    float distance = Vector3.Distance(transform.position, player.transform.position);
                    animator.SetBool("IsWalking", true);
                    if (distance < 5f)
                    {
                        bossState = BossState.Attacking;
                    }
                    break;
                case BossState.Attacking:
                    animator.SetBool("IsWalking", false);
                    animator.SetTrigger("Attack");
                    bossState = BossState.regular;
                    StartCoroutine(SpawnFireBall());
                    waitTimer = 3f;
                    break;
                default:
                    break;

            }
        }
    }

    public override void SpeedUp()
    {
        speed += 0.1f;
    }

    public override void HealthUp()
    {
        maxHp++;
        bossHP++;
    }
    public override void Damage(int damageValue)
    {
        if (!isInvincible)
        {

            StartCoroutine(InvincibleFrames());
            //StartCoroutine(SpawnDamage(damageValue));
            bossHP -= damageValue;
            if (bossHP < maxHp / 2)
            {
                int i = 0;
                while (i < 10)
                {
                    SpeedUp();
                    i++;
                }
            }
            if (bossHP <= 0)
            {
                GameManager.IsAudio2 = true;
                GameManager.scoreValue += 30;
                for (int i = 0; i < 10; i++)
                {
                    Instantiate(crystal, transform.position, Quaternion.identity);
                    Instantiate(font, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    Instantiate(orb, transform.position + new Vector3(0, -1, 0), Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            if (player.OnDamage())
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator InvincibleFrames()
    {
        isInvincible = true;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.4f);
        spriteRenderer.color = Color.white;
        isInvincible = false;
    }

    IEnumerator SpawnFireBall()
    {
        yield return new WaitForSeconds(0f);
        for (int i = 0; i < 5; i++)
        {
            double valueX = player.transform.position.x - transform.position.x;
            double valueY = player.transform.position.y - transform.position.y;
            double angle = ConvertRadiansToDegrees(Math.Atan2(valueY, valueX));
            Instantiate(fireBall, transform.position, Quaternion.Euler(0, 0, (float)angle));
            yield return new WaitForSeconds(0.2f);
        }

    }

    public static double ConvertRadiansToDegrees(double radians)
    {
        return Mathf.Rad2Deg * radians;
    }
}
