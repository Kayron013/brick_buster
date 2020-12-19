using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    Text txtRecordRound;
    Text txtRecordStreak;
    Text txtRecordTime;
    Text txtCurrentRound;
    Text txtCurrentStreak;
    Text txtCurrentTime;

    private void Awake()
    {
        var texts = gameObject.GetComponentsInChildren<Text>();
        txtRecordRound = texts[0];
        txtRecordStreak = texts[1];
        txtRecordTime = texts[2];
        txtCurrentRound = texts[3];
        txtCurrentStreak = texts[4];
        txtCurrentTime = texts[5];

        SetRecordRound("");
        SetRecordStreak("");
        SetRecordTime("");
        Reset();
    }
    public void Reset()
    {
        SetCurrentRound("1");
        SetCurrentStreak("0");
        SetCurrentTime("");
    }

    public void SetRecordRound(string rounds) => SetText(txtRecordRound, rounds);
    public void SetRecordStreak(string streak) => SetText(txtRecordStreak, streak);
    public void SetRecordTime(string timeSpan) => SetText(txtRecordTime, timeSpan);
    public void SetCurrentRound(string rounds) => SetText(txtCurrentRound, rounds);
    public void SetCurrentStreak(string streak) => SetText(txtCurrentStreak, streak);
    public void SetCurrentTime(string timeSpan) => SetText(txtCurrentTime, timeSpan);

    private void SetText(Text textObj, string text)
    {
        textObj.text = text;
    }
}
