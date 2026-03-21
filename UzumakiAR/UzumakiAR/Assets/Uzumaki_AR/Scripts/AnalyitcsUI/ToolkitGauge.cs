using UnityEngine;
using UnityEngine.UIElements;

public class ToolkitGauge : MonoBehaviour
{
    public UIDocument uiDoc;

    VisualElement[] segments;
    Label healthText;

    void Start()
    {
        var root = uiDoc.rootVisualElement;

        var bar = root.Q<VisualElement>("GaugeBar");
        healthText = root.Q<Label>("HealthText");

        segments = new VisualElement[10];

        for (int i = 0; i < 10; i++)
        {
            segments[i] = bar[i];
        }

        SetValue(72);
    }

    public void SetValue(int value)
    {
        int active = value / 10;

        for (int i = 0; i < 10; i++)
        {
            if (i < active)
            {
                if (value > 75)
                    segments[i].style.backgroundColor = new Color(0f,1f,0.5f);
                else if (value > 50)
                    segments[i].style.backgroundColor = Color.yellow;
                else
                    segments[i].style.backgroundColor = Color.red;
            }
            else
            {
                segments[i].style.backgroundColor = new Color(0.2f,0.2f,0.2f);
            }
        }

        healthText.text = value + "%";
    }
}
