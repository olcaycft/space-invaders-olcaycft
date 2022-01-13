using System;
using System.Collections;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    public Invader[] prefabs;
    [SerializeField] private AudioClip invaderMovement;

    public int rows = 5;
    public int columns = 11;
    private float width => 2f * (this.columns - 1);
    private float height => 2f * (this.rows - 1);
    private Vector2 center => new Vector2(-width / 2, -height / 2);
    private Vector3 rowPosition;
    private Vector3 invaderPosition;
    
    private Vector3 direction = Vector2.right;
    private Vector3 leftEdge => Camera.main.ViewportToWorldPoint(Vector3.zero);
    private Vector3 rightEdge => Camera.main.ViewportToWorldPoint(Vector3.right);

    private int count = 0;

    [SerializeField] private AnimationCurve speed;

    public int amountKilled { get; private set; }
    public int totalInvaders => columns * rows;
    public float percentKilled => (float) amountKilled / (float) totalInvaders;

    public static event Action<float> speedChanged;

    private void Awake()
    {
        SpawnAllInvaders();
        StartCoroutine(nameof(MoveInvaders));
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

                if (direction == Vector3.right && invader.position.x >= (rightEdge.x - 1f))
                {
                    ChangeRowAndDirection();
                    var position = transform.position;
                    position.x -= 1f;
                    transform.position = position;
                }
                else if (direction == Vector3.left && invader.position.x <= (leftEdge.x + 1f))
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

    private void InvaderKilled()
    {
        amountKilled++;
        speedChanged?.Invoke(speed.Evaluate(percentKilled));
    }
}