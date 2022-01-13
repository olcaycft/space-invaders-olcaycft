using System.Drawing;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private int score;
    [SerializeField] private int playerHp=3;
    [SerializeField]private AudioSource sfx;

    public void IncreaseScore(int scorePoint)
    {
        score += scorePoint;
    }

    public void DecreasePlayerHp()
    {
        playerHp--;
        if (playerHp<=0)
        {
            GameOver();
        }
        else
        {
            //reload scene
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }

    public void PlaySfx(AudioClip clip) => sfx.PlayOneShot(clip);
}
