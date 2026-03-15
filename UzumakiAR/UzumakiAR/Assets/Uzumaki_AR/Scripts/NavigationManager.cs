using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager : MonoBehaviour
{
    public static NavigationManager Instance;

    public GameObject[] screens;
    public int lastScreenIndex = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "UIScene")
        {
            RestoreScreen();
        }
    }

    // UI Screen change
    public void ShowScreen(int index)
    {
        lastScreenIndex = index;

        foreach (GameObject s in screens)
        {
            s.SetActive(false);
        }

        screens[index].SetActive(true);
    }

    // Open AR Scene
    public void OpenAR()
    {
        SceneManager.LoadScene("ImageTargetScene");
    }

    // Back to UI
    public void BackToUI()
    {
        SceneManager.LoadScene("UIScene");
    }

    // Restore last screen
    void RestoreScreen()
    {
        if (screens == null || screens.Length == 0)
            return;

        foreach (GameObject s in screens)
        {
            s.SetActive(false);
        }

        screens[lastScreenIndex].SetActive(true);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}