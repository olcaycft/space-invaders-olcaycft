using System;
using System.Collections;
using UnityEditor.Callbacks;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    public Invader[] prefabs;
    public int rows = 5;
    public int columns = 11;

    private Vector3 rowPosition;
    private Vector3 invaderPosition;

    private float width;
    private float height;
    private Vector2 center;
    private Vector3 direction = Vector2.right;
    private float speed = 1f;
    private Vector3 leftEdge;
    private Vector3 rightEdge;

    private void Awake()
    {
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        SpawnAllInvaders();
        StartCoroutine(nameof(MoveInvaders));
    }

    private void Update()
    {
    }

    private void SpawnAllInvaders()
    {
        width = 2f * (columns - 1);
        height = 2f * (rows - 1);
        center = new Vector2(-width / 2, -height / 2);
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
            yield return new WaitForSeconds(speed);
            transform.position += direction * 1f;
            foreach (Transform invader in transform)
            {
                if (!invader.gameObject.activeInHierarchy)
                {
                    continue;
                }

                if (direction == Vector3.right && invader.position.x >= (rightEdge.x-1f))
                {
                    ChangeRowAndDirection();
                    var position = transform.position;
                    position.x -= 1f;
                    transform.position = position;
                }
                else if (direction == Vector3.left && invader.position.x <= (leftEdge.x+1f))
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
}