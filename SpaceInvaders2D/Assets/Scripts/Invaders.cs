using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Invaders : MonoBehaviour
{
    [SerializeField] private Invader[] prefabs;
    [SerializeField] private Projectile missilePrefab;
    [SerializeField] private AudioClip invaderMovement;
    [SerializeField] private float missileAttackRate = 1f;

    [SerializeField] private int rows = 5;
    [SerializeField] private int columns = 11;
    private float width => 2f * (columns - 1);
    private float height => 2f * (rows - 1);
    private Vector2 center => new Vector2(-width / 2, -height / 2);
    private Vector3 rowPosition;
    private Vector3 invaderPosition;

    private Vector3 direction = Vector2.right;
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    private float leftEdgeX =>leftEdge.transform.position.x;
    private float rightEdgeX =>rightEdge.transform.position.x;

    [SerializeField] private AnimationCurve speed;

    public int amountKilled { get; private set; }
    [SerializeField] private int totalInvaders => columns * rows;
    [SerializeField] private float percentKilled => (float) amountKilled / (float) totalInvaders;
    [SerializeField] private float amountAlive => this.totalInvaders - this.amountKilled;

    public static event Action<float> speedChanged;

    private void Awake()
    {
        SpawnAllInvaders();
        StartCoroutine(nameof(MoveInvaders));
        StartCoroutine(nameof(MissileAttack));
        Invader.killed += InvaderKilled;
    }

    private void SpawnAllInvaders()
    {
        for (int row = 0; row < rows; row++)
        {
            rowPosition = new Vector3(center.x, center.y + (row * 2f), 0f);
            for (int col = 0; col < columns; col++)
            {
                Invader invader = Instantiate(prefabs[row], transform);
                invaderPosition = rowPosition;
                invaderPosition.x += col * 2f;
                invader.transform.localPosition = invaderPosition;
            }
        }
    }

    #region InvaderMovement

    private IEnumerator MoveInvaders()
    {
        while (true)
        {
            yield return new WaitForSeconds(speed.Evaluate(percentKilled));
            //GameManager.Instance.PlaySfx(invaderMovement);
            transform.position += direction * 1f;

            foreach (Transform invader in transform)
            {
                if (!invader.gameObject.activeInHierarchy)
                {
                    continue;
                }

                if (direction == Vector3.right && invader.position.x >= (rightEdgeX - 1f))
                {
                    ChangeRowAndDirection();
                    var position = transform.position;
                    position.x -= 1f;
                    transform.position = position;
                }
                else if (direction == Vector3.left && invader.position.x <= (leftEdgeX + 1f))
                {
                    ChangeRowAndDirection();
                    var position = transform.position;
                    position.x += 1f;
                    transform.position = position;
                }
            }
        }
    }

    private void ChangeRowAndDirection()
    {
        direction.x *= -1f;
        var position = transform.position;
        position.y -= 1f;
        transform.position = position;
    }

    #endregion

    private IEnumerator MissileAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(missileAttackRate);
            foreach (Transform invader in transform)
            {
                if (!invader.gameObject.activeInHierarchy)
                {
                    continue;
                }

                if (Random.value < (1f / (float) amountAlive)) //for chance logic
                {
                    Instantiate(missilePrefab, invader.position, Quaternion.identity);
                    break;
                }
            }
        }
    }


    private void InvaderKilled()
    {
        amountKilled++;
        speedChanged?.Invoke(speed.Evaluate(percentKilled));
    }
}