using UnityEngine;

public class StopMenu : MonoBehaviour
{
    [SerializeField] private GameObject Menu;

    public void ShowMenu()
    {
        Menu.SetActive(true);
    }

    public void HideMenu()
    {
        Menu.SetActive(false);
    }
}
