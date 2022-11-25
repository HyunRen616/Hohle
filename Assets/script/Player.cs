using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float speed = 1f;
    [SerializeField] BaseWeapon[] weapons;
    [SerializeField] GameObject pause;
    [SerializeField] GameManager gameManager;

    Animator animator;

    Material material;

    [SerializeField] PlayerCamera cam;

    SpriteRenderer spriteRenderer;

    public bool isAtacking = false;

    public int playerHP;
    public int maxHp = 3;

    internal int currentExp;
    internal int expToLevel = 5;
    internal int currentLevel = 0;

    bool isInvincible;

    public float GetHpRatio() 
    { 
        return playerHP / maxHp; 
    }

    void Start()
    {
        playerHP = maxHp;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
        //weapons[0].LevelUp();
    }

    internal void healUp(int TypeOfHeal)
    {
        StartCoroutine(HealingFrames());
        if (TypeOfHeal == 1 && playerHP <= (int)maxHp*3 / 4)
        {
            if(playerHP > (int)maxHp * 3 / 4) 
            {
                playerHP = (int)maxHp;
            }
            playerHP += (int)maxHp / 4;
        }
        else if (TypeOfHeal == 2 && playerHP <= (int)maxHp / 2)
        {
            if (playerHP > (int)maxHp / 2)
            {
                playerHP = (int)maxHp;
            }
            playerHP += (int)maxHp / 2;
        }

    }

    internal void SpeedUp()
    {
        speed+= 0.1F;
    }

    internal void MaxHealthUp()
    {
        maxHp++;
    }

    internal void AddExp()
    {
        currentExp++;

        if (currentExp == expToLevel)
        {
            currentExp = 0;
            expToLevel += 5;
            currentLevel++;

            Time.timeScale = 0;
            pause.SetActive(true);
            cam.Blur();
        }
    }

    internal void ColectAll()
    {
        GameObject[] results = GameObject.FindGameObjectsWithTag("Crystal");


        foreach (var crystal in results)
        {
            Vector3 destination = transform.position;
            Vector3 source = crystal.transform.position;
            Vector3 direction;

            direction = destination - source;

            direction.Normalize();


            var dx = source.x - destination.x;
            var dy = source.y - destination.y;


            if (Mathf.Abs(dx) <= 80 && Mathf.Abs(dy) <= 80)
            {
                Destroy(crystal);
                AddExp();
            }
        }
    }



    public bool OnDamage()
    {
        if (!isInvincible)
        {
            StartCoroutine(InvincibleFrames());
            cam.ShakeCamera();
            playerHP -= 1;
            if (playerHP <= 0)
            {
                SceneManager.LoadScene("Retry");
                Destroy(gameObject);
                return true;
            }
        }
        return false;

    }


    IEnumerator InvincibleFrames()
    {
        isInvincible = true;
        material.SetFloat("_Flash", 0.5f);
        yield return new WaitForSeconds(0.3f);
        material.SetFloat("_Flash", 0.0f);
        isInvincible = false;
    }

    IEnumerator HealingFrames()
    {
        material.SetFloat("_IsHealing", 1f);
        yield return new WaitForSeconds(0.5f);
        material.SetFloat("_IsHealing", 0.0f);
    }

    void Update()
    {
        if (Katana.isActive == true)
        {
            isAtacking = true;
            animator.SetBool("IsAtacking", isAtacking);
        }

        if (Katana.isActive == false)
        {
            isAtacking = false;
            animator.SetBool("IsAtacking", isAtacking);
        }

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");


        transform.position += new Vector3(inputX, inputY) * speed * Time.deltaTime;
        float scaleX = 1;
        if (inputX > 0)
        {
            scaleX = -1;
           
        }
        else if (inputX < 0)
        {
            scaleX = 1;
          
        }
        else if (inputX == 0)
        {
            scaleX = transform.localScale.x;
        }

        bool IsRunning = (inputY != 0 || inputX != 0 ? true : false);

        transform.localScale = new Vector3(scaleX, 1, 1);

        animator.SetBool("IsRunning", IsRunning);


    } 
}