using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class P2DAmountShower: MonoBehaviour {

    private int _currentAmount = 0;
    public int CurrentAmount
    {
        get { return _currentAmount; }
        set
        {
            _currentAmount = value;
            if(showMaxAmount)
                showText.text = _currentAmount.ToString() + "/" + maxAmount;
            else
                showText.text = _currentAmount.ToString();

        }
    }


    [SerializeField]
    Text showText;
    [SerializeField]
    bool showMaxAmount = false;

    int maxAmount = 1;
    [SerializeField]
    Image fillImage;

    public void SetAmount(int val,float time = 1,float delayTime = 0.5f)
    {
        DOTween.To(() => CurrentAmount, x => CurrentAmount = x, val, time).SetEase(Ease.OutCubic).SetDelay(delayTime);
        if(showMaxAmount && fillImage != null)
            DOTween.To(() => fillImage.fillAmount, x => fillImage.fillAmount = x, ((float)val)/maxAmount, time).SetEase(Ease.OutCubic).SetDelay(delayTime);
    }

    public void SetMaxAmount(int max)
    {
        maxAmount = max;
    }

  
}
