using System.Drawing;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private int score;
    [SerializeField] private int playerHp;

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
}
