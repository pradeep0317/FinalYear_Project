using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using System.Collections;

public class FirebaseReader : MonoBehaviour
{
    DatabaseReference reference;

    public TMP_Text dataText;     
    public TMP_Text timeText;     
    public TMP_Text dateText;     // NEW date text

    string temperature;
    string voltage;
    string current;
    string power;
    string energy;
    string frequency;
    string powerfactor;
    string time;
    string date;                  // NEW date variable

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
            temperature = args.Snapshot.Child("temperature").Value.ToString();
            voltage = args.Snapshot.Child("voltage").Value.ToString();
            current = args.Snapshot.Child("current").Value.ToString();
            power = args.Snapshot.Child("power").Value.ToString();
            energy = args.Snapshot.Child("energy").Value.ToString();
            frequency = args.Snapshot.Child("frequency").Value.ToString();
            powerfactor = args.Snapshot.Child("powerfactor").Value.ToString();
            time = args.Snapshot.Child("time").Value.ToString();
            date = args.Snapshot.Child("date").Value.ToString();   // NEW

            timeText.text = "Time : " + time;
            dateText.text = "Date : " + date;   // NEW

            StopAllCoroutines();
            StartCoroutine(ShowData());
        }
    }

    IEnumerator ShowData()
    {
        while (true)
        {
            dataText.text = "Temperature : " + temperature + " °C";
            yield return new WaitForSeconds(5);

            dataText.text = "Voltage : " + voltage + " V";
            yield return new WaitForSeconds(5);

            dataText.text = "Current : " + current + " A";
            yield return new WaitForSeconds(5);

            dataText.text = "Power : " + power + " W";
            yield return new WaitForSeconds(5);

            dataText.text = "Energy : " + energy + " kWh";
            yield return new WaitForSeconds(5);

            dataText.text = "Frequency : " + frequency + " Hz";
            yield return new WaitForSeconds(5);

            dataText.text = "Power Factor : " + powerfactor;
            yield return new WaitForSeconds(5);
        }
    }
}