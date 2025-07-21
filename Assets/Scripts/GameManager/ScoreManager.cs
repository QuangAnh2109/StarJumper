using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int CurrentScore { get; private set; } = 0;

    public TextMeshProUGUI scoreText;

    public void AddScore(int amount)
    {
        CurrentScore += amount;
        if(scoreText != null)
        {
            scoreText.text = "Score: " + CurrentScore;
        }
        else Debug.Log("Score: " + CurrentScore);
    }
}
