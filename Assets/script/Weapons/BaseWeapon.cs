using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    protected internal int level;
    internal int damage = 1;

    internal void LevelUp()
    {
        level++;
        if(level == 1)
        {
            gameObject.SetActive(true);
        }
    }

    internal void DamageUp()
    {
        damage++;
    }

}
