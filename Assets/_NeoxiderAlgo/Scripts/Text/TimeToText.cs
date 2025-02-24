using TMPro;
using UnityEngine;
using UnityEngine.Events;

public enum TimeFormat
{
    Milliseconds,
    Seconds,
    SecondsMilliseconds,
    Minutes,
    MinutesSeconds,
    Hours,
    HoursMinutes,
    HoursMinutesSeconds,
    Days,
    DaysHours,
    DaysHoursMinutes,
    DaysHoursMinutesSeconds,
}

[AddComponentMenu("_Neoxider/" + "Text/" + nameof(TimeToText))]
public class TimeToText : MonoBehaviour
{
    [SerializeField] private bool _zeroText = true;
    public TimeFormat timeFormat = TimeFormat.MinutesSeconds;
    public string startAddText;
    public string endAddText;
    public string separator = ":";
    public TMP_Text text;

    public UnityEvent OnEnd;

    private float lastTime;

    public void SetText(float time = 0)
    {
        if ((time == 0 && _zeroText) || time > 0)
            text.text = startAddText + FormatTime(time, timeFormat, separator) + endAddText;
        else
            text.text = "";

        if (lastTime != time && time == 0)
        {
            OnEnd?.Invoke();
        }

        lastTime = time;
    }

    public static string FormatTime(float time, TimeFormat format = TimeFormat.Seconds, string separator = ":")
    {
        return time.FormatTime(format, separator);
    }

    private void OnValidate()
    {
        if (text == null)
            text = GetComponent<TMP_Text>();
    }
}

public static class TimeExtensions
{
    public static string FormatTime(this float timeSeconds, TimeFormat format = TimeFormat.Seconds, string separator = ":")
    {
        int days = (int)(timeSeconds / 86400);
        int hours = (int)((timeSeconds % 86400) / 3600);
        int minutes = (int)((timeSeconds % 3600) / 60);
        int seconds = (int)(timeSeconds % 60);
        int milliseconds = (int)((timeSeconds - (int)timeSeconds) * 100);
        string formattedTime = "";

        switch (format)
        {
            case TimeFormat.Milliseconds:
                formattedTime = $"{milliseconds:D2}";
                break;
            case TimeFormat.SecondsMilliseconds:
                formattedTime = $"{seconds:D2}{separator}{milliseconds:D2}";
                break;
            case TimeFormat.Seconds:
                formattedTime = $"{seconds:D2}";
                break;
            case TimeFormat.Minutes:
                formattedTime = $"{minutes:D2}";
                break;
            case TimeFormat.MinutesSeconds:
                formattedTime = $"{minutes:D2}{separator}{seconds:D2}";
                break;
            case TimeFormat.Hours:
                formattedTime = $"{hours:D2}";
                break;
            case TimeFormat.HoursMinutes:
                formattedTime = $"{hours:D2}{separator}{minutes:D2}";
                break;
            case TimeFormat.HoursMinutesSeconds:
                formattedTime = $"{hours:D2}{separator}{minutes:D2}{separator}{seconds:D2}";
                break;
            case TimeFormat.Days:
                formattedTime = $"{days:D2}";
                break;
            case TimeFormat.DaysHours:
                formattedTime = $"{days:D2}{separator}{hours:D2}";
                break;
            case TimeFormat.DaysHoursMinutes:
                formattedTime = $"{days:D2}{separator}{hours:D2}{separator}{minutes:D2}";
                break;
            case TimeFormat.DaysHoursMinutesSeconds:
                formattedTime = $"{days:D2}{separator}{hours:D2}{separator}{minutes:D2}{separator}{seconds:D2}";
                break;
            default:
                formattedTime = "00";
                break;
        }

        return formattedTime;
    }
}