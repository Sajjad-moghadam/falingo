using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineManager : MonoBehaviour {

    const int maxCharInLine = 8;

    int lineSize;
    [SerializeField]
    CharacterButtonManager prefabButton;

    List<CharacterButtonManager> charButton = new List<CharacterButtonManager>();

    int myLineIndex;
    private void Awake()
    {
        
    }

    public void Init(int LineIndex)
    {
        myLineIndex = LineIndex;
        for (int i = 0; i < maxCharInLine; i++)
        {
            CharacterButtonManager ch = Instantiate(prefabButton, transform);
            ch.InitPosition(myLineIndex, i);
            charButton.Add(ch);
        }
    }

    // Use this for initialization
    void Start() {

    }

    public void ShowEmptyLine(int lineSize)
    {
        gameObject.SetActive(true);
        this.lineSize = lineSize;
        for (int i = 0; i < maxCharInLine; i++)
        {
            if (i < lineSize)
                charButton[i].ShowFade(lineSize);
            else
            {
                charButton[i].gameObject.SetActive(false);
            }
        }

    }

    public void ShowChar(char character, int index, int wordIndex)
    {
        charButton[index].ShowChar(character.ToString(), wordIndex);
    }

    public bool HasAllSelectedCorrectIndex(int index)
    {
        for (int i = 0; i < lineSize; i++)
        {
            if (charButton[i].IsSelected)
            {
                if (!charButton[i].HasWordIndex(index))
                    return false;
            }
        }

        return true;
    }

    public void DeSelectLine()
    {
        foreach (var item in charButton)
        {
            item.DeSelect();
        }
    }

    public void RemoveSelected()
    {
        foreach (var item in charButton)
        {
            item.RemoveSelected();
        }
    }

    public void HideLine()
    {
        gameObject.SetActive(false);
    }
}
