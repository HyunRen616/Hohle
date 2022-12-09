using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{
    
    private GameObject gameObjectPlayer;
    private Player player;
    [SerializeField] Image foreground;

    // Update is called once per frame

    void Start()
    {
        gameObjectPlayer = GameObject.FindGameObjectWithTag("Player");
        player = gameObjectPlayer.GetComponent<Player>();
    }
    void Update()
    {
        float hpRatio = (float)player.playerHP / player.maxHp;
        foreground.transform.localScale = new Vector3(hpRatio, 1, 1);
        if (player.playerHP <= 0)
        {
            Destroy(gameObject);
        }
    }

}