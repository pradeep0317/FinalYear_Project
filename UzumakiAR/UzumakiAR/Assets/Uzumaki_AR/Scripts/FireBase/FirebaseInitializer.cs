using UnityEngine;
using Firebase;
using Firebase.Extensions;

public class FirebaseInitializer : MonoBehaviour
{
    public static FirebaseInitializer Instance;

    public bool IsReady { get; private set; }

    public System.Action OnFirebaseReady;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitFirebase();
        }
        else Destroy(gameObject);
    }

    void InitFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                var app = FirebaseApp.DefaultInstance; 

                IsReady = true;

                Debug.Log(" Firebase Ready");

                OnFirebaseReady?.Invoke();
            }
            else
            {
                Debug.LogError(" Firebase Failed");
            }
        });
    }
}