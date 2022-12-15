using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SecondGameMenu : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] BaseWeapon[] weapons;
    [SerializeField] Enemy[] listEnemy;
    [SerializeField] Boss boss;
    [SerializeField] TMP_Text[] LevelText;
    [SerializeField] Player player;
    private int levelHealth = 0;
    private int levelSpeed = 0;

    private void Start()
    {
        for (int i = 0; i < TitleManager.saveData.levelweapon1; i++)
        {
            weapons[0].LevelUp();
            weapons[0].DamageUp();
        }
        for (int i = 0; i < TitleManager.saveData.levelweapon2; i++)
        {
            weapons[1].LevelUp();
            weapons[1].DamageUp();
        }
        for (int i = 0; i < TitleManager.saveData.levelweapon3; i++)
        {
            weapons[2].LevelUp();
            weapons[2].DamageUp();
        }
        for (int i = 0; i < TitleManager.saveData.levelweapon4; i++)
        {
            weapons[3].LevelUp();
            weapons[3].DamageUp();
        }
        for (int i = 0; i < TitleManager.saveData.levelweapon5; i++)
        {
            weapons[4].LevelUp();
            weapons[4].DamageUp();
        }
        for (int i = 0; i < TitleManager.saveData.levelweapon6; i++)
        {
            weapons[5].LevelUp();
            weapons[5].DamageUp();
        }
    }

    void Update()
    {
        LevelText[0].text = "Lv. " + weapons[0].level.ToString();
        LevelText[1].text = "Lv. " + weapons[2].level.ToString();
        LevelText[2].text = "Lv. " + weapons[4].level.ToString();
        LevelText[3].text = "Lv. " + levelSpeed.ToString();
        LevelText[4].text = "Lv. " + levelHealth.ToString();
    }

    public void OnKatanaClick()
    {
        LevelEnemy();
        weapons[0].LevelUp();
        weapons[0].DamageUp();
        if (weapons[0].level >= 3)
        {
            weapons[1].LevelUp();
            weapons[1].DamageUp();
        }
        Time.timeScale = 1;
        Camera.main.GetComponent<PlayerCamera>().Unblur();
        menu.SetActive(false);
    }

    public void OnScytheClick()
    {
        LevelEnemy();
        weapons[2].LevelUp();
        weapons[2].DamageUp();

        if (weapons[2].level >= 3)
        {
            weapons[3].LevelUp();
            weapons[3].DamageUp();
        }
        Time.timeScale = 1;
        Camera.main.GetComponent<PlayerCamera>().Unblur();
        menu.SetActive(false);
    }

    public void OnEnergyBallClick()
    {
        LevelEnemy();
        weapons[4].LevelUp();
        weapons[4].DamageUp();
        if (weapons[4].level >= 3)
        {
            weapons[5].LevelUp();
            weapons[5].DamageUp();
        }
        Time.timeScale = 1;
        Camera.main.GetComponent<PlayerCamera>().Unblur();
        menu.SetActive(false);
    }

    public void OnHealthClick()
    {
        LevelEnemy();
        levelHealth++;
        player.MaxHealthUp();
        Time.timeScale = 1;
        Camera.main.GetComponent<PlayerCamera>().Unblur();
        menu.SetActive(false);
    }

    public void OnSpeedClick()
    {
        LevelEnemy();
        levelSpeed++;
        player.SpeedUp();
        Time.timeScale = 1;
        Camera.main.GetComponent<PlayerCamera>().Unblur();
        menu.SetActive(false);
    }

    public void LevelEnemy()
    {
        foreach (var enemy in listEnemy)
        {
            enemy.HealthUp();
            enemy.SpeedUp();
        }
    }

    public void LevelBoss()
    {
        boss.HealthUp();
        boss.SpeedUp();

    }
}
