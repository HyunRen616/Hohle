using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    private GameObject gameObjectPlayer;
    [SerializeField] Player player; 
    [SerializeField] Image foreground;

    void Start()
    {
        gameObjectPlayer = GameObject.FindGameObjectWithTag("Player");
        player = gameObjectPlayer.GetComponent<Player>();
    }

    private void Update()
    {
        float expRatio = (float)player.currentExp / player.expToLevel;
        foreground.transform.localScale = new Vector3(expRatio, 1,1);
    }
}

