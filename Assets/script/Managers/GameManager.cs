using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


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
        SpawnEnemies(merman, 4, true);

        //SpawnEnemies(boss, 1, true);

        yield return new WaitForSeconds(5f);
        yield return new WaitForSeconds(6f);
        SpawnEnemies(zombie, 4, false);
        yield return new WaitForSeconds(6f);
        SpawnEnemies(newEnemy, 3, true);
        yield return new WaitForSeconds(6f);

        while (true)
        {
            int spawnCounter = 1;
            SpawnEnemies(merman, 4 + spawnCounter, true);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(zombie, 4 + spawnCounter, false);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(merman, 6, true);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(zombie, 6, false);
            yield return new WaitForSeconds(5f);

            SpawnEnemies(newEnemy, 7, true);
            yield return new WaitForSeconds(5f);

            SpawnEnemies(merman, 12, true);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(zombie, 14, false);

            SpawnEnemies(newEnemy, 4, false);
            yield return new WaitForSeconds(2f);

            SpawnEnemies(zombie, 5 * spawnCounter++, true);
            yield return new WaitForSeconds(5f);

            SpawnEnemies(newEnemy, 5, true);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(merman, 8, true);
            yield return new WaitForSeconds(3f);

            SpawnEnemies(merman, 10, true);
            yield return new WaitForSeconds(5f);

            SpawnEnemies(newEnemy, 12, true);
            yield return new WaitForSeconds(5f);

            SpawnEnemies(zombie, 18, false);
            yield return new WaitForSeconds(5f);

            SpawnEnemies(merman, 12, true);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(zombie, 14, false);

            SpawnEnemies(newEnemy, 5, false);
            yield return new WaitForSeconds(5f);
            SpawnEnemies(merman, 5 * spawnCounter++, true);

            yield return new WaitForSeconds(5f);

            SpawnEnemies(newEnemy, 5, true);
            yield return new WaitForSeconds(3f);

            SpawnEnemies(merman, 15, true);
            yield return new WaitForSeconds(5f);

            SpawnEnemies(boss, 1 + spawnCounter, true);
            yield return new WaitForSeconds(5f);

            spawnCounter++;


        }

    }

    void SpawnEnemies(GameObject enemyPrefab, int numberOfEnemies, bool isTrack)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            int dice = Random.Range(-8, 9);
            int dice2 = Random.Range(-24, -15);

            Vector3 spawnPosition = Random.insideUnitCircle.normalized * 8;

            if (!isTrack)
            {
                spawnPosition = new Vector3(dice2, dice, 0);
            }

            spawnPosition += player.transform.position;

            GameObject enemyObject = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            Enemy enemy = enemyObject.GetComponent<Enemy>();
            enemy.isTrack = isTrack;

        }

    }
}