using UnityEngine;
using UnityEngine.UIElements;
using System;

public class LogManager : MonoBehaviour
{
public UIDocument uiDoc;

private ScrollView logList;

void Start()
{
    var root = uiDoc.rootVisualElement;
    logList = root.Q<ScrollView>("LogList");

    if (MachineDataHandler.Instance != null)
    {
        MachineDataHandler.Instance.OnDataUpdated += HandleNewData;
    }
    //
    // // 🔥 TEST (remove later)
    // AddLog(
    //     "236", "1.2", "50", "0.5",
    //     "50", "0.75", "55",
    //     DateTime.Now.ToString("dd/MM/yyyy"),
    //     DateTime.Now.ToString("HH:mm:ss")
    // );
}

void HandleNewData()
{
    var data = MachineDataHandler.Instance;

    AddLog(
        data.Voltage.ToString(),
        data.Current.ToString(),
        data.Power.ToString(),
        data.Energy.ToString(),
        data.Frequency.ToString(),
        data.PF.ToString(),
        data.Temperature.ToString(),
        data.Date,
        data.Time
    );
}

void OnDestroy()
{
    if (MachineDataHandler.Instance != null)
    {
        MachineDataHandler.Instance.OnDataUpdated -= HandleNewData;
    }
}

public void AddLog(string voltage, string current, string power,
                   string energy, string frequency, string pf,
                   string temperature, string date, string time)
{
    float v = float.Parse(voltage);
    float c = float.Parse(current);
    float f = float.Parse(frequency);
    float pfVal = float.Parse(pf);
    float temp = float.Parse(temperature);

    var card = new VisualElement();
    card.style.backgroundColor = new Color(0.12f, 0.12f, 0.12f);
    card.style.marginBottom = 20;
    card.style.paddingLeft = 20;
    card.style.paddingRight = 20;
    card.style.paddingTop = 20;
    card.style.paddingBottom = 20;
    card.style.borderTopLeftRadius = 14;
    card.style.borderTopRightRadius = 14;
    card.style.borderBottomLeftRadius = 14;
    card.style.borderBottomRightRadius = 14;

    // HEADER
    var header = new VisualElement();
    header.style.flexDirection = FlexDirection.Row;
    header.style.justifyContent = Justify.SpaceBetween;

    var dateLabel = new Label(date);
    dateLabel.style.color = new Color(0.7f, 0.7f, 0.7f);
    dateLabel.style.fontSize = 20;

    var timeLabel = new Label(time);
    timeLabel.style.color = Color.white;
    timeLabel.style.fontSize = 24;
    timeLabel.style.unityFontStyleAndWeight = FontStyle.Bold;

    header.Add(dateLabel);
    header.Add(timeLabel);
    card.Add(header);

    // SECTION
    void AddSection(string title)
    {
        var section = new Label(title);
        section.style.color = new Color(0.23f, 0.48f, 0.99f);
        section.style.marginTop = 14;
        section.style.marginBottom = 8;
        section.style.fontSize = 22;
        section.style.unityFontStyleAndWeight = FontStyle.Bold;
        card.Add(section);
    }

    // COLOR LOGIC
    Color GetColor(float value, float min, float max)
    {
        if (value < min || value > max) return Color.red;
        if (value < min * 1.1f || value > max * 0.9f) return Color.yellow;
        return Color.white;
    }

    // ROW
    void AddRow(string label, string value, Color color)
    {
        var row = new VisualElement();
        row.style.flexDirection = FlexDirection.Row;
        row.style.justifyContent = Justify.SpaceBetween;
        row.style.marginTop = 8;

        var l = new Label(label);
        l.style.color = new Color(0.7f, 0.7f, 0.7f);
        l.style.fontSize = 20;

        var vLabel = new Label(value);
        vLabel.style.color = color;
        vLabel.style.fontSize = 24;
        vLabel.style.unityFontStyleAndWeight = FontStyle.Bold;

        row.Add(l);
        row.Add(vLabel);
        card.Add(row);
    }

    // ⚡ ELECTRICAL
    AddSection("ELECTRICAL");
    AddRow("Voltage", voltage + " V", GetColor(v, 200, 250));
    AddRow("Current", current + " A", GetColor(c, 0, 5));
    AddRow("Frequency", frequency + " Hz", GetColor(f, 48, 52));

    Color pfColor = pfVal < 0.8f ? Color.yellow : Color.white;
    AddRow("Power Factor", pf, pfColor);

    // ⚙️ POWER
    AddSection("POWER");
    AddRow("Power", power + " W", Color.white);
    AddRow("Energy", energy + " kWh", Color.white);

    // 🌡 THERMAL
    AddSection("THERMAL");
    Color tempColor = temp > 50 ? Color.red : Color.white;
    AddRow("Temperature", temperature + " °C", tempColor);

    // ADD TO TOP
    logList.Insert(0, card);
}

}
