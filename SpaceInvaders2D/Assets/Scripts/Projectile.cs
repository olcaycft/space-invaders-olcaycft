using System;
using UnityEngine;

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
        Destroy(gameObject);
    }
}