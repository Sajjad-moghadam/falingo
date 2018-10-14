using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PrizeTime
{
    EightHour = 8,
    TwentyForHour = 24
}

[RequireComponent(typeof(P2DCountDownTimer))]
public class PrizeScript : MonoBehaviour {
    const string timerKey = "PlayDateKey";

    P2DCountDownTimer myTimer;

    [SerializeField]
    PrizeTime prizeTime;

    public int Prize;

    [SerializeField]
    Button collectRewardButton;

    [SerializeField]
    Text prizeValueText;

    // Use this for initialization
    void Start ()
    {
        myTimer = GetComponent<P2DCountDownTimer>();
        prizeValueText.text = Prize.ToString();

        myTimer.OnTimerDoneEvent += MyTimer_OnTimerDoneEvent;

        ResetTimer();

	}

    private void MyTimer_OnTimerDoneEvent()
    {
        collectRewardButton.interactable = true;
        transform.DOScale(1.2f, 0.15f).SetLoops(8, LoopType.Yoyo).SetEase(Ease.Flash);
    }

    private void ResetTimer()
    {
        collectRewardButton.interactable = false;

        string dateString;

        if (!P2DSecurety.SecureLocalLoad(timerKey + prizeTime, out dateString))
        {
            SetNextDate();
            P2DSecurety.SecureLocalLoad(timerKey + prizeTime, out dateString);
        }

        DateTime end = Convert.ToDateTime(dateString);

        myTimer.SetTimer(end);
    }

    public void CollectRewardClick()
    {
        GameMng.Instance.AddDiamond(Prize);

        SetNextDate();
        ResetTimer();
    }

    private void SetNextDate()
    {
        DateTime newDate = DateTime.Now.AddSeconds((int)prizeTime);
        P2DSecurety.SecureLocalSave(timerKey + prizeTime, Convert.ToString(newDate));
    }
}
