using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;

public class FirebaseReader : MonoBehaviour
{
    DatabaseReference reference;

    public TMP_Text temperatureText;
    public TMP_Text timeText;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            reference = FirebaseDatabase.DefaultInstance.RootReference;

            ListenForData();
        });
    }

    void ListenForData()
    {
        FirebaseDatabase.DefaultInstance
        .GetReference("fan1/latest")
        .ValueChanged += HandleValueChanged;
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        if (args.Snapshot.Exists)
        {
            string temperature = args.Snapshot.Child("temperature").Value.ToString();
            string time = args.Snapshot.Child("time").Value.ToString();

            temperatureText.text = "Temperature : " + temperature + " °C";
            timeText.text = "Time : " + time;
        }
    }
}