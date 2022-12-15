using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class FBP2Spawner : BaseWeapon
{

    [SerializeField] GameObject fireBall;
    [SerializeField] GameObject playerAngle;
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
            yield return new WaitForSeconds(2f - (float)level / 2);
            float angle = playerAngle.transform.localScale.x;
            Instantiate(fireBall, transform.position, Quaternion.Euler(0, 0 ,90 - (90 * angle)));
        }
    }
}
