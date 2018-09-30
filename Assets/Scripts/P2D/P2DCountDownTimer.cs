using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public delegate void OnTimerDone();

public enum TimerType
{
    Day,
    Hour,
    Min
}

public class P2DCountDownTimer : MonoBehaviour
{

    public event OnTimerDone OnTimerDoneEvent;


    [SerializeField]
    Text timerText;

    public TimerType timerType = TimerType.Min;

    DateTime endTime;

    private void Start()
    {
    }


    public void SetTimer(int day, int hour, int min, int secound)
    {
        DateTime newDate = DateTime.Now.AddDays(day).AddHours(hour).AddMinutes(min).AddSeconds(secound);
        SetTimer(newDate);
    }

    public void SetTimer(DateTime end,bool autoStart = true)
    {
        endTime = end;

        StartTimer();

    }

    public void StartTimer()
    {
        StartCoroutine(timerTick());

    }

    WaitForSeconds oneSecound = new WaitForSeconds(1);
    TimeSpan ts;
    IEnumerator timerTick()
    {

        while (true)
        {
            yield return oneSecound;

            ts = endTime.Subtract(DateTime.Now);

            switch (timerType)
            {
                case TimerType.Day:
                    timerText.text = string.Format("{0}d:{1:00}:{2:00}:{3:00}", ts.Days , ts.Hours, ts.Minutes, ts.Seconds);
                    break;
                case TimerType.Hour:
                    timerText.text = string.Format("{0:00}:{1:00}:{2:00}", (ts.Days * 24) + ts.Hours, ts.Minutes, ts.Seconds);
                    break;
                case TimerType.Min:
                    timerText.text = string.Format("{0:00}:{1:00}",(ts.Days * 1440) + (ts.Hours * 60 )+ ts.Minutes, ts.Seconds);
                    break;
                default:
                    timerText.text = string.Format("{0}d:{1:00}:{2:00}:{3:00}", ts.Days, ts.Hours, ts.Minutes, ts.Seconds);
                    break;
            }


            if (ts.TotalSeconds <= 0)
            {
                timerText.text = "تمام";
                TimerDone();
                //transform.DOScale(1.2f, 0.15f).SetLoops(32, LoopType.Yoyo).SetEase(Ease.Flash);
                break;
            }

        }
    }

    public void TimerDone()
    {

        if (OnTimerDoneEvent != null)
        {
            OnTimerDoneEvent();
        }

    }
}
