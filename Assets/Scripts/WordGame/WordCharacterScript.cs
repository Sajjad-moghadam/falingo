using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WordCharacterScript : MonoBehaviour {

    [SerializeField]
    Text charText;

    CanvasGroup canvasGroup;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
    public void ShowEmpty()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 0.7f;
        charText.text = "";
    }

	public void SetCharacter(char character,float delay)
    {
        canvasGroup.DOFade(1, 0.2f);
        transform.DOScale(1.7f, 0.12f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.Flash).SetDelay(delay).OnComplete(()=>
        {
            charText.text = character.ToString();
        });
    }

    public void HideCharacter()
    {
        gameObject.SetActive(false);
    }
}
