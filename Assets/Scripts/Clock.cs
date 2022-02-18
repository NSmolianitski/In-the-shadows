using System;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] private Transform hourArrow;
    [SerializeField] private Transform minuteArrow;
    [SerializeField] private Transform secondArrow;

    private void Update()
    {
        SetCurrentTime();
    }

    private void SetCurrentTime()
    {
        var currentTime = DateTime.Now;
        float currentHour = currentTime.Hour % 12;
        float currentMinute = currentTime.Minute;
        float currentSecond = currentTime.Second;

        
        hourArrow.localRotation = Quaternion.Euler(0, -30 * (currentHour + (currentMinute / 60)), 0);
        minuteArrow.localRotation = Quaternion.Euler(0, -6 * (currentMinute + (currentSecond / 60)), 0);
        secondArrow.localRotation = Quaternion.Euler(0, -6 * currentSecond, 0);
    }
}
