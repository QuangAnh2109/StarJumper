using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; set;}

    public int Score { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        Score = 0;
    }

    public void AddScore(int points)
    {
        Score += points;
    }

    public void SubtractScore(int points)
    {
        Score -= points;
        if (Score < 0) Score = 0;
    }

    public void ResetScore()
    {
        Score = 0;
    }

    public void ResetGame()
    {
        ResetScore();
    }
}
