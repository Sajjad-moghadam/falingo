using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum PanelEffectType
{
    type1, type2
}

public delegate void OnPanelEnd();

public class P2DPanel : MonoBehaviour
{

    public event OnPanelEnd OnPanelEndEvent;

    void PanelEnd()
    {
        if (OnPanelEndEvent != null)
        {

            OnPanelEndEvent();
        }
    }

    bool isShow = false;
    public bool IsShow
    {
        get { return isShow; }
    }
    public float DeactiveTime = 1;
    float showTime = 0.5f;
    float elepsedTime = 0;
    float alfa = 0;

    CanvasGroup canvasGroup;
    Animator panelAnimator;

    Coroutine hidePanelRoutine;

    public bool shouldActive = false;
    public bool OnCenter = false;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        panelAnimator = GetComponent<Animator>();

        if (!shouldActive)
            hidePanelRoutine = StartCoroutine(DeActiveDelayed(DeactiveTime));
        else
            isShow = true;

    }

    // Use this for initialization
    void Start()
    {



    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (isShow && elepsedTime <= showTime)
        {
            elepsedTime += Time.unscaledTime;

            alfa = (elepsedTime / showTime);

            if (canvasGroup != null)
            {
                canvasGroup.alpha = alfa;
            }
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);

        if (hidePanelRoutine != null)
        {
            StopCoroutine(hidePanelRoutine);
        }


        isShow = true;
        showTime = 0.5f;

        if (OnCenter)
            transform.localPosition = Vector3.zero;

        if (panelAnimator != null)
        {
            panelAnimator.SetTrigger("InTrigger");

        }
    }


    /// <summary>
    /// *** Invoke deactive after 0.5 secound
    /// </summary>
    public void Hide(bool immediate = false)
    {
        if (isShow)
        {
            isShow = false;
            elepsedTime = 0;

            if (panelAnimator != null)
            {
                panelAnimator.SetTrigger("OutTrigger");
            }

            PanelEnd();

            if (!shouldActive)
            {
                if (immediate)
                {
                    hidePanelRoutine = StartCoroutine(DeActiveDelayed(0));
                }
                else
                {
                    hidePanelRoutine = StartCoroutine(DeActiveDelayed(DeactiveTime));
                }
            }
        }
    }

    IEnumerator DeActiveDelayed(float time)
    {
        yield return new WaitForSeconds(time);

        DeActive();

    }

    void DeActive()
    {
        alfa = 0;
        gameObject.SetActive(false);
    }

}
