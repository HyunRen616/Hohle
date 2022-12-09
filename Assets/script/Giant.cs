using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Giant : Enemy
{
    //private Animator animator;
    [SerializeField] GameObject dagger;

    Animator animator;
    enum GiantState
    {
        Idle = 0,
        Chasing = 1,
        Attacking = 2
    }

    GiantState giantState = GiantState.Idle;
    float waitTimer = 1f;
    float waitTimer2 = 5f;

    protected override void Start()
    {
        animator = GetComponent<Animator>();
        base.Start();
    }


    protected override void Update()
    {

        switch (giantState)
        {
            case GiantState.Idle:
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0)
                {
                    giantState = GiantState.Chasing;
                }
                break;
            case GiantState.Chasing:
                base.Update();
                float distance = Vector3.Distance(transform.position, player.transform.position);
                animator.SetBool("IsWalking", true);
                if (distance < 5f)
                {
                    giantState = GiantState.Attacking;
                }
                break;
            case GiantState.Attacking:
                animator.SetBool("IsWalking", false);
                animator.SetTrigger("Attack");
                giantState = GiantState.Idle;
                StartCoroutine(SpawnKnife());
                waitTimer = 3f;              
                break;
            default:
                break;
        }
    }
    public override void Damage(int damageValue)
    {
        giantState = GiantState.Idle;
        base.Damage(damageValue);
    }
    IEnumerator SpawnKnife()
    {
        yield return new WaitForSeconds(1f);
        double valueX = player.transform.position.x - transform.position.x;
        double valueY = player.transform.position.y - transform.position.y;
        double angle = ConvertRadiansToDegrees(Math.Atan2(valueY, valueX));


        Instantiate(dagger, transform.position, Quaternion.Euler(0, 0, (float)angle));
    }

    public static double ConvertRadiansToDegrees(double radians)
    {
        return Mathf.Rad2Deg * radians;
    }
}
