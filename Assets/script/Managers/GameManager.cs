using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text orbCounter;
    [SerializeField] TMP_Text score;
    [SerializeField] GameObject merman;
    [SerializeField] GameObject zombie;
    [SerializeField] GameObject player;
    [SerializeField] GameObject newEnemy;
    [SerializeField] GameObject boss;

    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;


    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource audioSource2;

    [SerializeField] SimpleObjectPool[] arrayPoolEnemy;

    public static bool IsAudio = false;
    public static bool IsAudio2 = false;
    public static int scoreValue = 0;


    private void Update()
    {
        if (IsAudio == true)
        {
            audioSource.Play();
            IsAudio = false;
        }
        if (IsAudio2 == true)
        {
            audioSource2.Play();
            IsAudio2 = false;
        }
        int minutes;
        int seconds = (int)Time.timeSinceLevelLoad;
        minutes = seconds / 60;
        if (minutes >= 1)
        {
            seconds -= minutes * 60;
        }
        if (seconds < 10 && minutes < 10)
        {
            timerText.text = "0" + minutes.ToString() + ":" + "0" + seconds.ToString();
        }
        else if (seconds < 10)
        {
            timerText.text = minutes.ToString() + ":" + "0" + seconds.ToString();
        }
        else if (minutes < 10)
        {
            timerText.text = "0" + minutes.ToString() + ":" + seconds.ToString();
        }
        //orbCounter.text = TitleManager.saveData.orbCount.ToString();
        score.text = scoreValue.ToString();
        if(minutes >= 1 && TitleManager.saveData.IsLevel1 == true)
        {
            SceneManager.LoadScene("GameLevel2");
            TitleManager.saveData.IsLevel2 = true;
            TitleManager.saveData.IsLevel1 = false;
        }

        
    }
    void Start()
    {
        if (TitleManager.saveData.IsWhiteUnlocked == true)
        {
            player1.SetActive(true);
        }

        else if(TitleManager.saveData.IsBlackUnlocked == true)
        {
            player2.SetActive(true);
        }

        StartCoroutine(SpawnEnemyCoroutine());
    }
    private IEnumerator SpawnEnemyCoroutine()
    {
        yield return new WaitForSeconds(3f);
        SpawnEnemies(merman, 4, true, arrayPoolEnemy[0]);

        //SpawnEnemies(boss, 1, true, arrayPoolEnemy[3]);

        yield return new WaitForSeconds(5f);
        yield return new WaitForSeconds(6f);
        SpawnEnemies(zombie, 4, true, arrayPoolEnemy[1]);
        yield return new WaitForSeconds(6f);
        SpawnEnemies(newEnemy, 3, true, arrayPoolEnemy[2]);
        yield return new WaitForSeconds(6f);

        while (true)
        {
            int spawnCounter = 1;
            SpawnEnemies(merman, 4, true, arrayPoolEnemy[0]);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(zombie, 4, false, arrayPoolEnemy[1]);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(merman, 4, true, arrayPoolEnemy[0]);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(zombie, 6, false, arrayPoolEnemy[1]);
            yield return new WaitForSeconds(5f);

            SpawnEnemies(newEnemy, 7, true, arrayPoolEnemy[2]);
            yield return new WaitForSeconds(5f);

            SpawnEnemies(merman, 7, true, arrayPoolEnemy[0]);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(zombie, 14, false, arrayPoolEnemy[1]);

            SpawnEnemies(newEnemy, 4, true, arrayPoolEnemy[2]);
            yield return new WaitForSeconds(2f);

            SpawnEnemies(zombie, 5 * spawnCounter, false, arrayPoolEnemy[1]);
            yield return new WaitForSeconds(5f);

            SpawnEnemies(newEnemy, 5, true, arrayPoolEnemy[2]);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(merman, 8, true, arrayPoolEnemy[0]);
            yield return new WaitForSeconds(3f);

            SpawnEnemies(merman, 10, true, arrayPoolEnemy[0]);
            yield return new WaitForSeconds(5f);
            yield return new WaitForSeconds(5f);

            SpawnEnemies(newEnemy, 12, true, arrayPoolEnemy[2]);
            yield return new WaitForSeconds(5f);

            SpawnEnemies(zombie, 18, false, arrayPoolEnemy[1]);
            yield return new WaitForSeconds(5f);
            yield return new WaitForSeconds(5f);

            SpawnEnemies(merman, 12, true, arrayPoolEnemy[0]);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(zombie, 14, false, arrayPoolEnemy[1]);

            SpawnEnemies(merman, 12, true, arrayPoolEnemy[0]);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(zombie, 14, false, arrayPoolEnemy[1]);

            SpawnEnemies(newEnemy, 5, true, arrayPoolEnemy[2]);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(merman, 5, true, arrayPoolEnemy[0]);

            yield return new WaitForSeconds(5f);

            SpawnEnemies(newEnemy, 5, true, arrayPoolEnemy[2]);
            yield return new WaitForSeconds(3f);

            SpawnEnemies(merman, 15, true, arrayPoolEnemy[0]);
            yield return new WaitForSeconds(5f);

            SpawnEnemies(boss, 1 , true, arrayPoolEnemy[3]);
            yield return new WaitForSeconds(5f);

            spawnCounter++;


        }

    }

    void SpawnEnemies(GameObject enemyPrefab, int numberOfEnemies, bool isTrack, SimpleObjectPool poolthingy)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            int dice = Random.Range(-8, 9);
            int dice2 = Random.Range(-24, -15);

            Vector3 spawnPosition = Random.insideUnitCircle.normalized * 8;

            if (!isTrack)
            {
                //spawnPosition = new Vector3(dice2, dice, 0);
                spawnPosition = Random.insideUnitCircle.normalized * 5;
            }

            spawnPosition += player.transform.position;
            GameObject enemyObject = poolthingy.GetObject();
            enemyObject.transform.position = spawnPosition;
            Enemy enemy = enemyObject.GetComponent<Enemy>();
            enemyObject.SetActive(true);
            enemy.enemyHP = enemy.maxHp;
            enemy.isTrack = isTrack;

            //GameObject enemyObject = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            //Enemy enemy = enemyObject.GetComponent<Enemy>();
            //enemy.isTrack = isTrack;

        }

    }
}