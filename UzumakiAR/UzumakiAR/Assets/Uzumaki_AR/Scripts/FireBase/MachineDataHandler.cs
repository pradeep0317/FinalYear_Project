using UnityEngine;

public class MachineDataHandler : MonoBehaviour
{
    public static MachineDataHandler Instance;

    // SENSOR DATA
    public float Voltage, Current, Power, Energy;
    public float Frequency, PF, Temperature;
    public string Date, Time;

    // ML DATA
    public bool Anomaly;
    public string FaultType;
    public float HealthScore;
    public string HealthStatus;

    public System.Action OnDataUpdated;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateFullData(
        float voltage, float current, float power, float energy,
        float frequency, float pf, float temperature,
        string date, string time,
        bool anomaly, string fault, float healthScore, string healthStatus)
    {
        Voltage = voltage;
        Current = current;
        Power = power;
        Energy = energy;
        Frequency = frequency;
        PF = pf;
        Temperature = temperature;

        Date = date;
        Time = time;

        Anomaly = anomaly;
        FaultType = fault;
        HealthScore = healthScore;
        HealthStatus = healthStatus;

        Debug.Log(" Data Updated (Global)");

        OnDataUpdated?.Invoke();
    }
}