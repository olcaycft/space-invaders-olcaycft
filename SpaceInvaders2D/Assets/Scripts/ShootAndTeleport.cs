using UnityEngine;

public class ShootAndTeleport : MonoBehaviour
{
    [SerializeField] private Projectile laserPrefab;
    private bool isLaserActive;
    
    [SerializeField] private AudioClip shootSound;

    
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    private float leftEdgeX =>leftEdge.transform.position.x;
    private float rightEdgeX =>rightEdge.transform.position.x;

    private void Awake()
    {
        Laser.playerProjectileHit += ChangeLaserActive;
        PlayerMovementSystem.playerShooting += Shoot;
    }

    private void Update()
    {
        PlayerTeleportation();
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

    private void PlayerTeleportation()
    {
            if (transform.position.x <= leftEdgeX)
            {
                var position = transform.position;
                position.x = rightEdgeX;
                transform.position = position;
            }
            else if (transform.position.x >= rightEdgeX)
            {
                var position = transform.position;
                position.x = leftEdgeX;
                transform.position = position;
            }
    }
}