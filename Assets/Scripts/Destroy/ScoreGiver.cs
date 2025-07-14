using UnityEngine;

[RequireComponent(typeof(BaseDestroyController))]
public class ScoreGiver : MonoBehaviour
{
    [SerializeField] private int scoreOnDeath = 0;

    private void Start()
    {
        BaseDestroyController destroyController = GetComponent<BaseDestroyController>();
        destroyController.DestroyAction += GiveScore;
    }

    private void GiveScore()
    {
        ScoreManager.Instance?.AddScore(scoreOnDeath);
    }
}