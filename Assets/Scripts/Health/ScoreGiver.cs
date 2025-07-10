using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public class ScoreGiver : MonoBehaviour
{
    [SerializeField] private int scoreOnDeath = 0;

    private void Start()
    {
        HealthSystem health = GetComponent<HealthSystem>();
        health.OnDeath += GiveScore;
    }

    private void GiveScore()
    {
        ScoreManager.Instance?.AddScore(scoreOnDeath);
    }
}
