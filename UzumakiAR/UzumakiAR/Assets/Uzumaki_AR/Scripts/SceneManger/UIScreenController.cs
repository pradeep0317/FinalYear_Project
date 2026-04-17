using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class UIScreenController : MonoBehaviour
{
    public UIDocument uiDocument;

    private VisualElement root;

    private Dictionary<string, VisualElement> screens;
    private Stack<string> screenHistory = new Stack<string>();
    
    float lastPF = -999f;
    

    void Start()
    {
        root = uiDocument.rootVisualElement;

        //  Register all screens
        screens = new Dictionary<string, VisualElement>()
        {
            {"HomeScreen", root.Q<VisualElement>("HomeScreen")},
            {"MachineListUI", root.Q<VisualElement>("MachineListUI")},
            {"MachineDetails", root.Q<VisualElement>("MachineDetails")},
            {"LogUI", root.Q<VisualElement>("LogUI")},
            {"AnalyticsUI", root.Q<VisualElement>("AnalyticsUI")}
        };

        //  Default screen
        ShowScreen("HomeScreen", false);

        RegisterButtons();
    }
    

    void Update()
    {
            UpdateDateTime();
            float pf = MachineDataHandler.Instance.PF;

            if (pf != lastPF)
            {
                UpdateMachineStatus(pf);
                lastPF = pf;
            }
    }

    void RegisterButtons()
    {
        //  Home → Machine List
        root.Q<Button>("ViewMachineButton").clicked += () =>
        {
            ShowScreen("MachineListUI");
        };

        //  Machine List → Details
        root.Q<Button>("MachineImageButton").clicked += () =>
        {
            ShowScreen("MachineDetails");
        };

        //  Details → Log
        root.Q<Button>("LogButton").clicked += () =>
        {
            ShowScreen("LogUI");
        };

        //  Details → Analytics
        root.Q<Button>("AnalyticsButton").clicked += () =>
        {
            ShowScreen("AnalyticsUI");
        };
        root.Q<Button>("OpenWebAnalytics").clicked += () =>
        {
            Application.OpenURL("https://pradeep0317.github.io/UzumakiARWeb/");

        };

        //  Details → Live Data Scene
        root.Q<Button>("LIveDataViewButton").clicked += () =>
        {
            SceneStateManager.Instance.returnScreen = "MachineDetails";
            SceneManager.LoadScene("ImageTargetScene");
        };

        //  Back Buttons (Common)
        foreach (var btn in root.Query<Button>("BackButton").ToList())
        {
            btn.clicked += GoBack;
        }
    }

    //  Show Screen
    public void ShowScreen(string screenName, bool addToHistory = true)
    {
        foreach (var screen in screens.Values)
        {
            screen.style.display = DisplayStyle.None;
        }

        screens[screenName].style.display = DisplayStyle.Flex;

        if (addToHistory)
        {
            screenHistory.Push(screenName);
        }
    }
    void UpdateDateTime()
    {
        var labels = root.Query<Label>("DateAndTime").ToList();

        string currentTime = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

        foreach (var label in labels)
        {
            label.text = currentTime;
        }
    }
    void UpdateMachineStatus(float powerFactor)
    {
        var statusLabels = root.Query<Label>("StatusTxt").ToList();

        foreach (var label in statusLabels)
        {
            if (powerFactor > 0)
            {
                label.text = "RUNNING";
                label.style.color = new StyleColor(Color.green);
            }
            else
            {
                label.text = "NOT RUNNING";
                label.style.color = new StyleColor(Color.red);
            }
        }
    }

    //  Back Logic
    public void GoBack()
    {
        if (screenHistory.Count > 1)
        {
            //Debug.Log("this method is called");
            // Remove current
            screenHistory.Pop();

            // Go to previous
            string previous = screenHistory.Peek();

            ShowScreen(previous, false);
        }
    }
}