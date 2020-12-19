using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeKeeper
{
    private static TimeSpan timeSpan = new TimeSpan();
    private static bool isClockRunning = false;
    private static DateTime lastStart;

    public static TimeSpan TimeSpan
    {
        get
        {
            var span = timeSpan;
            if (isClockRunning)
            {
                span = timeSpan + (DateTime.Now - lastStart);
            }
            return span;
        }
    }

    public static void StartClock()
    {
        isClockRunning = true;
        lastStart = DateTime.Now;
    }

    public static void StopClock()
    {
        isClockRunning = false;
        var diff = DateTime.Now - lastStart;
        timeSpan += diff;
    }

    public static void ResetClock()
    {
        timeSpan = new TimeSpan();
    }
}
