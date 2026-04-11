using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LiveDataUI : MonoBehaviour
{
    public Button backButton;

    void Start()
    {
        backButton.onClick.AddListener(GoBack);
    }

    void GoBack()
    {
        SceneManager.LoadScene("UIMainScreen");
    }
}