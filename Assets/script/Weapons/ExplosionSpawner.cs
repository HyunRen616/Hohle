using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSpawner : BaseWeapon
{
    [SerializeField] GameObject playerAngle;
    [SerializeField] GameObject explosion;
    [SerializeField] SimpleObjectPool pool;

    // Start is called before the first frame update
    void Start()
    {
        playerAngle = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SpawnScytheCoroutine());
    }

    IEnumerator SpawnScytheCoroutine()
    {

        while (true)
        {
            yield return new WaitForSeconds(2.5f - (float)level / 2);
            Vector3 spawnPosition = Random.insideUnitCircle.normalized * 5;
            spawnPosition += playerAngle.transform.position;
            float angle = Random.Range(0, 360);
            Instantiate(explosion, spawnPosition, Quaternion.identity);
        }
    }
}
