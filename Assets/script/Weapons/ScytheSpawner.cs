using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheSpawner : BaseWeapon
{
    [SerializeField] GameObject scythe;
    [SerializeField] SimpleObjectPool pool;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnScytheCoroutine());
    }

    IEnumerator SpawnScytheCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f - (float)level/2);
            float angle = Random.Range(0, 360);
            Instantiate(scythe, transform.position, Quaternion.Euler(0, 0, angle));
        }
    }
}
