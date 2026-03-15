using UnityEngine;
using TMPro;
using System.Collections;

public class RealTimeDateTime : MonoBehaviour
{
    public TMP_Text dateText;
    public TMP_Text timeText;

    void Start()
    {
        StartCoroutine(UpdateTime());
    }

    IEnumerator UpdateTime()
    {
        while(true)
        {
            System.DateTime now = System.DateTime.Now;

            dateText.text = now.ToString("dd/MM/yyyy");
            timeText.text = now.ToString("HH.mm.ss");

            yield return new WaitForSeconds(1f);
        }
    }
}