using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] Image foreground;

    // Update is called once per frame
    void Update()
    {
        transform.position = enemy.transform.position + new Vector3(0, 0.9f, 0);
        float hpRatio = (float)enemy.enemyHP / enemy.maxHp;
        foreground.transform.localScale = new Vector3(hpRatio, 1, 1);
        if (enemy.enemyHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
