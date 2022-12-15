using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chalise : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            player.healUp(1);
            Destroy(gameObject);
        }
    }
}
