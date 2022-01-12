using System;
using Unity.Mathematics;
using UnityEditor.SearchService;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Projectile laserPrefab;
    private bool isLaserActive;

    private float speed = 5f;
    private Vector3 leftEdge => Camera.main.ViewportToWorldPoint(Vector3.zero);
    private Vector3 rightEdge => Camera.main.ViewportToWorldPoint(Vector3.right);

    private void Awake()
    {
        Projectile.projectileHit += ChangeLaserActive;
    }

    private void Update()
    {
        PlayerSideMovement();
        PlayerShooting();
    }


    private void PlayerShooting()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (!isLaserActive)
        {
            Instantiate(laserPrefab, transform.position, Quaternion.identity);
            isLaserActive = true;
        }
    }

    private void ChangeLaserActive()
    {
        isLaserActive = false;
    }

    private void PlayerSideMovement()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (transform.position.x <= leftEdge.x)
            {
                var position = transform.position;
                position.x = rightEdge.x;
                transform.position = position;
            }
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (transform.position.x >= rightEdge.x)
            {
                var position = transform.position;
                position.x = leftEdge.x;
                transform.position = position;
            }
        }
    }
}