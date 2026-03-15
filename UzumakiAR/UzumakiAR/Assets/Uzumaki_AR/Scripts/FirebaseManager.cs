using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance;
    public DatabaseReference db;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                if (task.Result == DependencyStatus.Available)
                {
                    db = FirebaseDatabase.DefaultInstance.RootReference;
                    Debug.Log("Firebase Ready");
                }
            });
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public DatabaseReference GetReference(string path)
    {
        return FirebaseDatabase.DefaultInstance.GetReference(path);
    }
}