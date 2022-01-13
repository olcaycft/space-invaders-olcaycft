using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float speed;

    private void Update()
    {
        ProjectileMovement();
    }

    private void ProjectileMovement()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}