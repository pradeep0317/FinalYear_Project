using UnityEngine;
using Firebase.Database;
using TMPro;
using System.Collections;

public class FirebaseReader : MonoBehaviour
{
    DatabaseReference reference;

    public TMP_Text dataText;
    public TMP_Text timeText;
    public TMP_Text dateText;

    string temperature;
    string voltage;
    string current;
    string power;
    string energy;
    string frequency;
    string powerfactor;
    string time;
    string date;

    bool coroutineStarted = false;

    void Start()
    {
        reference = FirebaseManager.Instance.GetReference("fan1/latest");
        reference.ValueChanged += HandleValueChanged;

        StartCoroutine(ShowData()); // start once
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
            date = args.Snapshot.Child("date").Value.ToString();

            timeText.text = "Time : " + time;
            dateText.text = "Date : " + date;
        }
    }

    IEnumerator ShowData()
    {
        while (true)
        {
            dataText.text = "Temperature : " + temperature + " °C";
            yield return new WaitForSeconds(2);

            dataText.text = "Voltage : " + voltage + " V";
            yield return new WaitForSeconds(2);

            dataText.text = "Current : " + current + " A";
            yield return new WaitForSeconds(2);

            dataText.text = "Power : " + power + " W";
            yield return new WaitForSeconds(2);

            dataText.text = "Energy : " + energy + " kWh";
            yield return new WaitForSeconds(2);

            dataText.text = "Frequency : " + frequency + " Hz";
            yield return new WaitForSeconds(2);

            dataText.text = "Power Factor : " + powerfactor;
            yield return new WaitForSeconds(2);
        }
    }

    void OnDestroy()
    {
        if (reference != null)
        {
            reference.ValueChanged -= HandleValueChanged;
        }
    }
}
