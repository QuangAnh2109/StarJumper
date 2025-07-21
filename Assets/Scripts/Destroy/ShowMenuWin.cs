using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(BaseDestroyController))]
public class ShowMenuFinish : MonoBehaviour
{
    [SerializeField] private string sceneLevel = "0";
    [SerializeField] private GameObject Menu;

    private void Start()
    {
        BaseDestroyController destroyController = GetComponent<BaseDestroyController>();
        destroyController.DestroyAction += SaveLevel;
    }

    private void SaveLevel()
    {
        GameManager.Instance?.Level.Add(sceneLevel);
        Menu.SetActive(true);
    }
}