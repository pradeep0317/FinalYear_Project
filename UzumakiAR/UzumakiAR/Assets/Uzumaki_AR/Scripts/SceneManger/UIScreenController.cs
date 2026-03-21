using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIScreenController : MonoBehaviour
{
    public UIDocument uiDocument;

    private VisualElement root;
    private VisualElement[] screens;

    void Start()
    {
        root = uiDocument.rootVisualElement;

        // 🔥 get screens
        screens = new VisualElement[]
        {
            root.Q<VisualElement>("Screen1"),
            root.Q<VisualElement>("Screen2"),
            root.Q<VisualElement>("Screen3"),
            root.Q<VisualElement>("Screen4"),
            root.Q<VisualElement>("Screen5")
        };

        // 🔥 restore last screen
       // ShowScreen(ScreenStateManager.Instance.LastScreenIndex);

        // Example: Screen4 button → scene change
        // Button goToSceneBtn = root.Q<Button>("GoToSceneBtn");
        // goToSceneBtn.clicked += () =>
        // {
        //     ScreenStateManager.Instance.LastScreenIndex = 3; // Screen4 index
        //
        //     SceneManager.LoadScene("ImageTargetScene");
        // };
    }

    public void ShowScreen(int index)
    {
        for (int i = 0; i < screens.Length; i++)
        {
            screens[i].style.display =
                (i == index) ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}