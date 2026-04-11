using UnityEngine;
using UnityEngine.UIElements;

public class ToolkitGauge : MonoBehaviour
{
    public UIDocument uiDoc;

    VisualElement[] segments;
    Label healthText;

    // 🔥 ADD THESE
    Label statusLabel;
    Label anomalyLabel;
    Label faultLabel;

    void Start()
    {
        var root = uiDoc.rootVisualElement;

        var bar = root.Q<VisualElement>("GaugeBar");
        healthText = root.Q<Label>("HealthText");

        // 🔥 GET LABELS
        statusLabel = root.Q<Label>("StatusLabel");
        anomalyLabel = root.Q<Label>("AnomalyLabel");
        faultLabel = root.Q<Label>("FaultLabel");

        segments = new VisualElement[10];

        for (int i = 0; i < 10; i++)
        {
            segments[i] = bar[i];
        }

        // CONNECT TO DATA HANDLER
        if (MachineDataHandler.Instance != null)
        {
            MachineDataHandler.Instance.OnDataUpdated += UpdateUIFromData;
        }

        //SetValue(72);
    }

    void UpdateUIFromData()
    {
        var data = MachineDataHandler.Instance;

        // ✅ Gauge update
        int value = Mathf.RoundToInt(data.HealthScore);
        SetValue(value);

        // ✅ STATUS
        statusLabel.text = data.HealthStatus;

        if (data.HealthStatus == "GOOD")
            statusLabel.style.color = new Color(0, 1, 0.5f);
        else if (data.HealthStatus == "WARNING")
            statusLabel.style.color = Color.yellow;
        else
            statusLabel.style.color = Color.red;

        // ✅ ANOMALY
        anomalyLabel.text = data.Anomaly ? "False" : "True";

        anomalyLabel.style.color = data.Anomaly ? Color.red : new Color(0, 1, 0.5f);

        // ✅ FAULT TYPE
        faultLabel.text = string.IsNullOrEmpty(data.FaultType) ? "None" : data.FaultType;
    }

    void OnDestroy()
    {
        if (MachineDataHandler.Instance != null)
        {
            MachineDataHandler.Instance.OnDataUpdated -= UpdateUIFromData;
        }
    }

    public void SetValue(int value)
    {
        value = Mathf.Clamp(value, 0, 100);

        int active = value / 10;

        for (int i = 0; i < 10; i++)
        {
            if (i < active)
            {
                if (value > 75)
                    segments[i].style.backgroundColor = new Color(0f, 1f, 0.5f);
                else if (value > 50)
                    segments[i].style.backgroundColor = Color.yellow;
                else
                    segments[i].style.backgroundColor = Color.red;
            }
            else
            {
                segments[i].style.backgroundColor = new Color(0.2f, 0.2f, 0.2f);
            }
        }

        healthText.text = value + "%";
    }
}