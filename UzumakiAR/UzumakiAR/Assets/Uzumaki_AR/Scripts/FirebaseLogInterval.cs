using UnityEngine;
using Firebase.Database;
using TMPro;
using System.Collections;

public class FirebaseLogInterval : MonoBehaviour
{
    DatabaseReference reference;

    public Transform content;
    public GameObject logPrefab;

    [Header("Fetch Interval (Seconds)")]
    public float fetchInterval = 5f;

    Coroutine fetchCoroutine;

    void Start()
    {
        reference = FirebaseManager.Instance.GetReference("fan1/latest");
        fetchCoroutine = StartCoroutine(FetchLogs());
    }

    IEnumerator FetchLogs()
    {
        while (true)
        {
            if (reference == null) yield break;

            var task = reference.GetValueAsync();

            yield return new WaitUntil(() => task.IsCompleted);

            if (task.Exception != null)
            {
                Debug.LogError(task.Exception);
            }
            else
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    string temp = snapshot.Child("temperature").Value.ToString();
                    string volt = snapshot.Child("voltage").Value.ToString();
                    string curr = snapshot.Child("current").Value.ToString();
                    string power = snapshot.Child("power").Value.ToString();
                    string energy = snapshot.Child("energy").Value.ToString();
                    string freq = snapshot.Child("frequency").Value.ToString();
                    string time = snapshot.Child("time").Value.ToString();

                    GameObject log = Instantiate(logPrefab, content);

                    LogItemUI item = log.GetComponent<LogItemUI>();

                    int serial = content.childCount;

                    item.SetData(serial, volt, curr, power, energy, freq, temp, time);
                }
            }

            yield return new WaitForSeconds(fetchInterval);
        }
    }

    void OnDestroy()
    {
        if (fetchCoroutine != null)
        {
            StopCoroutine(fetchCoroutine);
        }
    }
}
