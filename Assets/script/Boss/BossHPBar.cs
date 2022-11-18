using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour
{
    [SerializeField] Boss boss;
    [SerializeField] Image foreground;

    // Update is called once per frame
    void Update()
    {
        float hpRatio = (float)boss.bossHP / boss.maxHp;
        foreground.transform.localScale = new Vector3(hpRatio, 1, 1);
        if (boss.bossHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
