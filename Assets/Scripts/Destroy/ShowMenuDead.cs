using UnityEngine;

[RequireComponent(typeof(BaseDestroyController))]
public class ShowMenuWin : MonoBehaviour
{
    [SerializeField] private GameObject Menu;

    private void Start()
    {
        BaseDestroyController destroyController = GetComponent<BaseDestroyController>();
        destroyController.DestroyAction += ShowMenu;
    }

    private void ShowMenu()
    {
        Menu.SetActive(true);
    }
}