using UnityEngine;
using Firebase.Database;
using TMPro;

public class MachineStatusChecker : MonoBehaviour
{
    DatabaseReference reference;

    public TMP_Text statusText;

    void Start()
    {
        reference = FirebaseManager.Instance.GetReference("fan1/latest");
        reference.ValueChanged += HandleValueChanged;
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (this == null || !gameObject.activeInHierarchy)
            return;

        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        if (args.Snapshot.Exists)
        {
            string currentStr = args.Snapshot.Child("current").Value.ToString();

            float current = 0f;
            float.TryParse(currentStr, out current);

            if (current > 0)
            {
                statusText.text = "Running";
                statusText.color = Color.green;
            }
            else
            {
                statusText.text = "Not Running";
                statusText.color = Color.red;
            }
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