using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterButtonManager : MonoBehaviour
{
    static Color greenColor = new Color(0.22f, 0.7f, 0.22f);

    [SerializeField]
    Image charImage;
    [SerializeField]
    public Text charText;

    int myLine, myIndex;

    int wordIndex;
    CanvasGroup canvasGroup;
    bool isSelected = false;
    public bool IsSelected { get { return isSelected; } }
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void InitPosition(int lineNumber, int index)
    {
        myLine = lineNumber;
        myIndex = index;
    }

    public void ShowChar(string character, int wordIndex)
    {
        gameObject.SetActive(true);
        this.wordIndex = wordIndex;
        charImage.color = Color.white;
        transform.localScale = Vector3.zero;
        canvasGroup.alpha = 1;
        transform.DOScale(1, 0.6f);
        charText.text = character;
    }

    public void ShowFade(int wordGameSize)
    {
        gameObject.SetActive(true);
        charText.text = "";
        charText.fontSize = 128 / wordGameSize;
        canvasGroup.alpha = 0;
    }


    public void DeSelect()
    {

        charImage.color = Color.white;
        isSelected = false;
    }

    public void RemoveSelected()
    {
        if (isSelected)
        {
            transform.DOScale(0, 0.5f).OnComplete(() => { /*gameObject.SetActive(false);*/ });

        }
    }

    public bool HasWordIndex(int index)
    {
        if (index == wordIndex)
            return true;

        return false;
    }

    public void OnPointerEnter(BaseEventData eventData)
    {
        //PointerEventData data = (PointerEventData)eventData;

        CheckCharacter();

    }

    private void CheckCharacter()
    {
        if (!isSelected && WordsGameManager.Instance.StartFindNewWord && IsNearLastButton())
        {
            isSelected = true;
            charImage.color = greenColor;
            WordsGameManager.Instance.SetNextChar(this);
        }
    }

    private bool IsNearLastButton()
    {
        CharacterButtonManager last = WordsGameManager.Instance.lastSelectedButton;

        if (last == null || (Mathf.Abs(last.myLine - myLine) < 2) && (Mathf.Abs(last.myIndex - myIndex) < 2))
        {
            return true;
        }

        return false;
    }

    public void OnPointerDown(BaseEventData eventData)
    {
        WordsGameManager.Instance.StartFindNewWord = true;
        CheckCharacter();
    }

    public void OnPointerUp(BaseEventData eventData)
    {
        WordsGameManager.Instance.EndFindWord();
    }

}
