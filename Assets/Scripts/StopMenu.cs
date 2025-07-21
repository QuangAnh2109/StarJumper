using UnityEngine;

public class StopMenu : MonoBehaviour
{
    [SerializeField] private GameObject Menu;

    public void ShowMenu()
    {
        Time.timeScale = 0f;
        Menu.SetActive(true);
    }

    public void HideMenu()
    {
        Time.timeScale = 1f;
        Menu.SetActive(false);
    }
}
