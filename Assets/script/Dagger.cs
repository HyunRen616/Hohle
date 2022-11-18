using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * 5 * Time.deltaTime; ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            if (player.OnDamage())
            {
                Destroy(gameObject);
            }
        }
    }
}
