using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AchivmentType
{
    ExcelentLesson,
    ExcelentCategory,
    ExamTime,
    CorrectInRow,
    ExcelentExam,
    PassLesson
}

public class AchivmentScript : MonoBehaviour
{

    public AchivmentType achivmentType;
    public int requestedAmount;
    public int Prize;
    public string myMessage = "عالی بود";


    [SerializeField]
    Button collectRewardButton;

    [SerializeField]
    Text prizeValueText;

    private void Start()
    {
        if (collectRewardButton == null)
            Debug.LogError("CollectRewardButton is null on " + gameObject.name);
        if (collectRewardButton == null)
            Debug.LogError("prizeValueText is null on " + gameObject.name);

        prizeValueText.text = Prize.ToString();


    }

    private void ChangeButton()
    {
        prizeValueText.transform.parent.gameObject.SetActive(false);
        collectRewardButton.transform.Find("Text").GetComponent<Text>().text = "مشاهده";
        collectRewardButton.interactable = true;
    }

    private void OnEnable()
    {
        if (IsCollected())
        {
            ChangeButton();
        }
        else if (IsReady2Collect())
        {
            transform.DOScale(1.2f, 0.15f).SetLoops(8, LoopType.Yoyo).SetEase(Ease.Flash);
            collectRewardButton.interactable = true;
        }

    }

    public void Open2Collect()
    {
        Sprite mySprite = null;
        try
        {
            mySprite = transform.Find("column3").Find("Image").GetComponent<Image>().sprite;
        }
        catch { }

        SetReady2Collect();
        Setting.popupNotification.Show("اچیومنت جدیدی باز شد", mySprite);
        GameMng.Instance.SendAchivmentEarn(achivmentType.ToString() + "_" + requestedAmount);
    }

    const string Ready2CollectKey = "Ready2Collect";
    public bool IsReady2Collect()
    {
        bool val = false;
        P2DSecurety.SecureLocalLoad(achivmentType.ToString() + Ready2CollectKey + requestedAmount, out val);
        return val;
    }

    private void SetReady2Collect()
    {
        P2DSecurety.SecureLocalSave(achivmentType.ToString() + Ready2CollectKey + requestedAmount, true);
    }

    public bool IsCollected()
    {
        bool val = false;
        P2DSecurety.SecureLocalLoad(achivmentType.ToString() + requestedAmount, out val);
        return val;
    }

    private void SetCollected()
    {
        P2DSecurety.SecureLocalSave(achivmentType.ToString() + requestedAmount, true);
        ChangeButton();
    }

    public void OnCollectClick()
    {
        if(!IsCollected())
        {
            GameMng.Instance.AddDiamond(Prize);

            SetCollected();

        }
        else
        {
            Sprite sprite = transform.Find("column3").Find("Image").GetComponent<Image>().sprite;
            GameMng.Instance.ShowAchivmentBigPanel(myMessage, sprite);
        }
      
    }

}
