using UnityEngine;

[RequireComponent(typeof(BaseDestroyController))]
public class SaveScene : MonoBehaviour
{
    [SerializeField] private string sceneLevel = "0";

    private void Start()
    {
        BaseDestroyController destroyController = GetComponent<BaseDestroyController>();
        destroyController.DestroyAction += SaveLevel;
    }

    private void SaveLevel()
    {
        GameManager.Instance?.Level.Add(sceneLevel);
        Time.timeScale = 0;
        Debug.Log($"Level {sceneLevel} saved to GameManager's Level list.");
    }
}