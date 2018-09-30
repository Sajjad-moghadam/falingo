using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using Soomla.Store;
using System;
using System.Collections.Generic;


public delegate void OnUnlock();

public class LockPanelScript : MonoBehaviour {

    public event OnUnlock OnUnlockEvent;

    void Unlocked()
    {
        if (OnUnlockEvent != null)
        {

            OnUnlockEvent();
        }
    }

    [SerializeField]
    LocksName lockName;

    [SerializeField]
    GameObject breakedLock;
    List<Image> breakedLockParts = new List<Image>();

    public LockInfo lockInfo;

    Button lockButton;
    Image lockImage;
    Text lockText;

    Animator myAnimator;
    AudioSource breakSoundEffect;

    void Awake()
    {
        lockImage = transform.Find("Image").GetComponent<Image>();
        lockText = lockImage.transform.Find("Text").GetComponent<Text>();
        lockButton = GetComponent<Button>();

        myAnimator = GetComponent<Animator>();
        breakSoundEffect = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start ()
    {
        breakedLock.SetActive(false);

        foreach (Transform item in breakedLock.transform)
        {
            breakedLockParts.Add(item.GetComponent<Image>());
        }

        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();

        Init();

    }

    public void SetLockName(LocksName name)
    {
        lockName = name;

        //StopAllCoroutines();
        //StartCoroutine(Init());
        Init();

    }

    void Init()
    {

        lockInfo = LockManager.Locks[lockName];

        if(Setting.lastLevel >= lockInfo.levelRequierd2Unlock && lockInfo.Unlock == 0)
        {
            lockInfo.Unlock = 1;
        }

        CheckUnlockState();
        
    }

    private void CheckUnlockState()
    {
        if (lockInfo.Unlock == 0)
        {
            gameObject.SetActive(true);

            lockText.text = lockInfo.levelRequierd2Unlock.ToString();
        }
        else if (lockInfo.Unlock == 1)
        {
            if(myAnimator != null)
            {
                gameObject.SetActive(true);
                StartCoroutine(BreakLock());
            }
            else
            {
                gameObject.SetActive(false);

            }
            lockInfo.Unlock = 2;
        }
        else
        {
            gameObject.SetActive(false);

        }
    }

    public void OnClick()
    {
        //Setting.MessegeBox.SetMessege(("با رسیدن به " + lockInfo.levelRequierd2Unlock + " " + Setting.levelName + " این بخش باز میشود").faConvert(),("آیا  " + lockInfo.discription + " الان باز شود؟").faConvert(),GameAssets.currencyName.faConvert() + " " +  lockInfo.cost,"باز کردن قفل".faConvert());
        Setting.MessegeBox.OnOkButtonClickEvent += MessegeBox_OnOkButtonClickEvent;
    }

    private void MessegeBox_OnOkButtonClickEvent()
    {
        //if(StoreInventory.GetItemBalance(GameAssets.Game_CURRENCY_ITEM_ID) >= lockInfo.cost)
        //{
        //    StoreInventory.TakeItem(GameAssets.Game_CURRENCY_ITEM_ID, lockInfo.cost);
        //    lockInfo.Unlock = 1;
        //    CheckUnlockState();
        //}
        //else
        //{
        //    StartCoroutine(NoMoney());
        //}
    }

    IEnumerator NoMoney()
    {
        yield return new WaitForSeconds(0.6f);

        //Setting.MessegeBox.SetMessege(("برای باز کردن این آیتم  " + lockInfo.cost + " " + GameAssets.currencyName + "لازم داریم").faConvert(),"","",(GameAssets.currencyName + " کافی نیست").faConvert());
        Setting.MessegeBox.OnOkButtonClickEvent += InAppPurchesPanelShow;
        Setting.MessegeBox.OnCancelButtonClickEvent += InAppPurchesPanelShow;

    }

    public void SetInteractable(bool value)
    {
        lockButton.interactable = value;
    }

    private void InAppPurchesPanelShow()
    {
        //Setting.inAppPurches.Show();
    }

    IEnumerator  BreakLock()
    {
        yield return new WaitForSeconds(0.5f);

        lockImage.enabled = false;
        breakedLock.SetActive(true);
        lockText.enabled = false;

        foreach (Image item in breakedLockParts)
        {
            item.enabled = true;
        }

        Unlocked();

        if(breakSoundEffect != null)
            breakSoundEffect.Play();

        myAnimator.SetTrigger("BreakTrigger");
    }

    private void AfterLockBreak()
    {
        foreach (Image item in breakedLockParts)
        {
            item.enabled = false;
        }
        transform.Find("Image").GetComponent<Image>().color = Color.white;
        gameObject.SetActive(false);
    }
}
