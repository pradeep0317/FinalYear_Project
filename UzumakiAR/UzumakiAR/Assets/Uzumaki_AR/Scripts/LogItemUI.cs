using TMPro;
using UnityEngine;

public class LogItemUI : MonoBehaviour
{
    public TMP_Text voltageText;
    public TMP_Text currentText;
    public TMP_Text powerText;
    public TMP_Text energyText;
    public TMP_Text frequencyText;
    public TMP_Text temperatureText;
    public TMP_Text timeText;
    public TMP_Text serialText;

    public void SetData(int serial,string volt, string curr, string power, string energy,
        string freq, string temp, string time)
    {
        voltageText.text = "Voltage : " + volt + " V";
        currentText.text = "Current : " + curr + " A";
        powerText.text = "Power : " + power + " W";
        energyText.text = "Energy : " + energy + " kWh";
        frequencyText.text = "Frequency : " + freq + " Hz";
        temperatureText.text = "Temp : " + temp + " °C";
        timeText.text = "Time : " + time;
        serialText.text = serial.ToString();
    }
}