using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "ScriptableObjects/EntityData")]
public abstract class EntityData : ScriptableObject
{
    public int Score { get;private set; }

    protected virtual void OnEnable()
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
        if (Score < 0)
        {
            Score = 0;
        }
    }

    public void ResetScore()
    {
        Score = 0;
    }
}