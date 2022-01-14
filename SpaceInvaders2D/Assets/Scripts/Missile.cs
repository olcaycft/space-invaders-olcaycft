using System;
using UnityEngine;

public class Missile : Projectile
{
    /*private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.DecreasePlayerHp();
        }

        Destroy(gameObject);
    }
*/
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.DecreasePlayerHp();
        }
        Destroy(gameObject);
    }
}
