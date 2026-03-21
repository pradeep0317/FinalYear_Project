using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;

public class FirebaseDataFetcher : MonoBehaviour
{
    public static FirebaseDataFetcher Instance;

    private DatabaseReference dbRef;

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

    private void Start()
    {
        FirebaseInitializer.Instance.OnFirebaseReady += InitDB;
    }

    void InitDB()
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;

        Debug.Log(" Listening Firebase...");

        dbRef.Child("fan1").ValueChanged += OnDataChanged;
    }

    void OnDataChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        var root = args.Snapshot;

        var latest = root.Child("latest");
        var ml = root.Child("ml");

        float voltage = GetFloat(latest, "voltage");
        float current = GetFloat(latest, "current");
        float power = GetFloat(latest, "power");
        float energy = GetFloat(latest, "energy");
        float frequency = GetFloat(latest, "frequency");
        float pf = GetFloat(latest, "powerfactor");
        float temperature = GetFloat(latest, "temperature");

        string date = GetString(latest, "date");
        string time = GetString(latest, "time");

        bool anomaly = GetBool(ml, "anomaly");
        string fault = GetString(ml, "fault_type");
        float healthScore = GetFloat(ml, "health_score");
        string healthStatus = GetString(ml, "health_status");

        //  Send to handler
        MachineDataHandler.Instance.UpdateFullData(
            voltage, current, power, energy,
            frequency, pf, temperature,
            date, time,
            anomaly, fault, healthScore, healthStatus
        );
    }

    float GetFloat(DataSnapshot data, string key)
    {
        if (data.HasChild(key) && data.Child(key).Value != null)
            return float.Parse(data.Child(key).Value.ToString());
        return 0f;
    }

    string GetString(DataSnapshot data, string key)
    {
        if (data.HasChild(key) && data.Child(key).Value != null)
            return data.Child(key).Value.ToString();
        return "";
    }

    bool GetBool(DataSnapshot data, string key)
    {
        if (data.HasChild(key) && data.Child(key).Value != null)
            return bool.Parse(data.Child(key).Value.ToString());
        return false;
    }
}