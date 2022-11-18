using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] bool IsBoss;
    public GameObject player;
    [SerializeField] GameObject crystal;
    [SerializeField] GameObject magnet;
    [SerializeField] GameObject orb;
    [SerializeField] GameObject chalise;
    [SerializeField] GameObject font;
    [SerializeField] float speed = 1f;
    public int enemyHP = 1;
    public int maxHp = 1;

    SpriteRenderer spriteRenderer;

    public bool isTrack = true;
    protected bool isInvincible;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }


    // Update is called once per frame
    protected virtual void Update()
    {
        if (player != null)
        {

            Vector3 destination = player.transform.position;
            Vector3 source = transform.position;
            Vector3 direction;
            if (!isTrack)
            {
                direction = new Vector3(1, 0, 0);
            }
            else
            {
                direction = destination - source;
            }
            direction.Normalize();
            transform.position += direction * Time.deltaTime * speed;

            int scaleX = 1;

            if (direction.x > 0)
            {
                scaleX = -1;
            }
            transform.localScale = new Vector3(scaleX, 1, 1);
        }
    }

    internal void SpeedUp()
    {
        speed+= 0.1f;
    }

    internal void HealthUp()
    {
        maxHp ++;
        enemyHP++;
    }


    public virtual void Damage(int damageValue)
    {
        if (!isInvincible)
        {
            StartCoroutine(InvincibleFrames());
            enemyHP -= damageValue;
            if (enemyHP <= 0)
            {
                GameManager.IsAudio = true;
                GameManager.scoreValue++;
                Instantiate(crystal, transform.position, Quaternion.identity);
                int randomNumber = UnityEngine.Random.Range(0, 20);
                if (randomNumber > 10 && randomNumber < 15)
                {
                    if(!TitleManager.saveData.Upgrade2) 
                    { 
                        Instantiate(chalise, transform.position + new Vector3(0, 1, 0), Quaternion.identity); 
                    }
                    else
                    {
                        Instantiate(font, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    }
                }

                else if (randomNumber > 7 && randomNumber < 9)
                {
                    Instantiate(orb, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                }
                else if (randomNumber > 8 && TitleManager.saveData.Upgrade1)
                {
                    Instantiate(magnet, transform.position + new Vector3(0, -1, 0), Quaternion.identity);
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
        yield return new WaitForSeconds(0.25f);
        spriteRenderer.color = Color.white;
        isInvincible = false;
    }

}
