using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class QRFrameBlink : MonoBehaviour
{
    public GameObject qrFrame;     // QR frame UI
    public float blinkInterval = 0.5f; 
    public float totalTime = 5f;   

    void Start()
    {
        StartCoroutine(BlinkFrame());
    }

    IEnumerator BlinkFrame()
    {
        float timer = 0f;

        while (timer < totalTime)
        {
            qrFrame.SetActive(!qrFrame.activeSelf); // toggle
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        qrFrame.SetActive(false); // finally hide
    }
    public void Back()
    {
        NavigationManager.Instance.BackToUI();
    }
}