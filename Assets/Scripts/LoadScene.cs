using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // General method to load scenes based on build index
    public void byIndex(int sceneIndex)
    {

        Console.WriteLine($"Loading scene with index: {sceneIndex}");
        SceneManager.LoadScene(sceneIndex);
    }

    // Method to change the scene to the specified scene name
    public void byName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        Application.Quit();

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
