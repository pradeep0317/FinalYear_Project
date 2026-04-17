using UnityEngine;
using TMPro;

public class MachineDataUI : MonoBehaviour
{
    //  SENSOR UI
    public TMP_Text voltageTxt;
    public TMP_Text currentTxt;
    public TMP_Text powerTxt;
    public TMP_Text energyTxt;
    public TMP_Text frequencyTxt;
    public TMP_Text pfTxt;
    public TMP_Text temperatureTxt;
    public TMP_Text timeTxt;

    //  ML UI
    public TMP_Text anomalyTxt;
    public TMP_Text faultTxt;
    public TMP_Text healthScoreTxt;
    public TMP_Text healthStatusTxt;
    public TMP_Text healthStatusTxt1;

    void Update()
    {
        //  Singleton access
        var data = MachineDataHandler.Instance;

        if (data == null) return;

        //  SENSOR DATA
        voltageTxt.text = $"Voltage : {data.Voltage:F1} V";
        currentTxt.text = $"Current : {data.Current:F2} A";
        powerTxt.text = $"Power : {data.Power:F1} W";
        energyTxt.text = $"Energy : {data.Energy:F2} kWh";
        frequencyTxt.text = $"Frequency : {data.Frequency:F0} Hz";
        pfTxt.text = $"Power Factor : {data.PF:F2}";
        temperatureTxt.text = $"Temperature : {data.Temperature:F1} °C";
        timeTxt.text = $"Time : {data.Time}";

        //  ML DATA
        anomalyTxt.text = $"Anomaly : {(data.Anomaly ? "Yes" : "No")}";
        faultTxt.text = $"Fault : {data.FaultType}";
        healthScoreTxt.text = $"Health Score : {data.HealthScore:F0} %";
        healthStatusTxt.text = $"Health Status : {data.HealthStatus}";
        healthStatusTxt1.text = data.HealthStatus;
    }
}