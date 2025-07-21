using UnityEngine;

[RequireComponent(typeof(BaseDestroyController))]
public class ScoreGiver : MonoBehaviour
{
    [SerializeField] private int scoreOnDeath = 0;
    [SerializeField] private GameObject scoreManager;

    private ScoreManager scoreManagerInstance;
    private void Start()
    {
        BaseDestroyController destroyController = GetComponent<BaseDestroyController>();
        destroyController.DestroyAction += GiveScore;
        scoreManagerInstance = scoreManager?.GetComponent<ScoreManager>();
    }

    private void GiveScore()
    {
        scoreManagerInstance.AddScore(scoreOnDeath);
    }
}