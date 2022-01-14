using System;
using Unity.Mathematics;
using UnityEditor.SearchService;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Projectile laserPrefab;
    private bool isLaserActive;
    
    [SerializeField] private AudioClip shootSound;

    private float speed = 5f;
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    private float leftEdgeX =>leftEdge.transform.position.x;
    private float rightEdgeX =>rightEdge.transform.position.x;

    private void Awake()
    {
        Laser.playerProjectileHit += ChangeLaserActive;
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
            //GameManager.Instance.PlaySfx(shootSound);
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
            if (transform.position.x <= leftEdgeX)
            {
                var position = transform.position;
                position.x = rightEdgeX;
                transform.position = position;
            }
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (transform.position.x >= rightEdgeX)
            {
                var position = transform.position;
                position.x = leftEdgeX;
                transform.position = position;
            }
        }
    }
}