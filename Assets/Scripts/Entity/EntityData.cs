using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "ScriptableObjects/EntityData")]
public abstract class EntityData : ScriptableObject
{
    [SerializeField]
    private int score = 0;

    public void AddScore(int points)
    {
        score += points;
    }

    public void SubtractScore(int points)
    {
        score -= points;
        if (score < 0)
        {
            score = 0;
        }
    }
    public int GetScore()
    {
        return score;
    }
}
