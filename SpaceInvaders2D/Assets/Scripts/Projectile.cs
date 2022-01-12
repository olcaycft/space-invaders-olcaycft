using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float speed;
    public static  event Action projectileHit;

    private void Update()
    {
        ProjectileMovement();
    }

    private void ProjectileMovement()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        projectileHit?.Invoke();
        if (col.gameObject.CompareTag("Invader01"))
        {
            GameManager.Instance.IncreaseScore(30);
        }
        else if (col.gameObject.CompareTag("Invader02"))
        {
            GameManager.Instance.IncreaseScore(20);
        }
        else if (col.gameObject.CompareTag("Invader03"))
        {
            GameManager.Instance.IncreaseScore(10);
        }
        else if (col.gameObject.CompareTag("MysteryShip"))
        {
            int number = Random.Range(1,4);
            if (number==1)
            {
                GameManager.Instance.IncreaseScore(50);
            }
            else if (number==2)
            {
                GameManager.Instance.IncreaseScore(100);
            }
            else if (number==3)
            {
                GameManager.Instance.IncreaseScore(200);
            }
        }
        Destroy(gameObject);
    }
}