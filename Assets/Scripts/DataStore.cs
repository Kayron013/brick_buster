using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStore
{
    static int FewestRounds { get; set; } = int.MaxValue;
    static int BestStreak { get; set; } = int.MinValue;
    static TimeSpan BestTime { get; set; } = TimeSpan.MaxValue;

    // Returns true if reported round is lower that previous lowest
    public static bool ReportRounds(int rounds)
    {
        if(rounds < FewestRounds)
        {
            FewestRounds = rounds;
            return true;
        }
        return false;
    }

    // Returns true if reported streak is higher that previous highest
    public static bool ReportStreak(int streak)
    {
        if (streak > BestStreak)
        {
            BestStreak = streak;
            return true;
        }
        return false;
    }

    // Returns true if reported time is lower that previous lowest
    public static bool ReportTime(TimeSpan time)
    {
        if (time < BestTime)
        {
            BestTime = time;
            return true;
        }
        return false;
    }
}
