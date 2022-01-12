using System;
using System.Collections;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public Sprite[] animationSprites;
    public float animationTime;
    private int animationFrame;
    
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer spriteRenderer => _spriteRenderer = GetComponent<SpriteRenderer>();

    private void Awake()
    {
        StartCoroutine(nameof(AnimateSprite));
    }


    private IEnumerator AnimateSprite()
    {
        while (true)
        {
            animationFrame++;
            if (animationFrame >=animationSprites.Length)
            {
                animationFrame = 0;
            }

            spriteRenderer.sprite = animationSprites[animationFrame];
            yield return new WaitForSeconds(animationTime);
        }
    }
}
