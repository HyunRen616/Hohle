using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSSpawner : BaseWeapon
{
    [SerializeField] GameObject scythe;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnScytheCoroutine());
    }

    IEnumerator SpawnScytheCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f - (float)level / 4);
            float angle = Random.Range(0, 360);
            Instantiate(scythe, transform.position, Quaternion.Euler(0, 0, angle));
        }
    }
}
