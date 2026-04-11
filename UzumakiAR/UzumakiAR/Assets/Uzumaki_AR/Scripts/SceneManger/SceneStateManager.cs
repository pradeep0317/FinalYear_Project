using UnityEngine;

public class SceneStateManager : MonoBehaviour
{
    public static SceneStateManager Instance;

    public string returnScreen;

    void Awake()
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
}