using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum WordGameSize
{
    Size_2_2 = 2,
    Size_3_3 = 3,
    Size_4_4 = 4,
    Size_5_5 = 5,
    Size_6_6 = 6,
    Size_7_7 = 7,
    Size_8_8 = 8,
}
public class WordsGameManager : SingletonMahsa<WordsGameManager>
{

    const int maxLines = 8;
    const int maxWords = 7;


    public Question question;
    int findedWords = 0;
    int questionLines;

    [SerializeField]
    LineManager LinePrefab;
    [SerializeField]
    WordLineManager wordPrefab;

    [SerializeField]
    Text selectedStringText;

    [SerializeField]
    Transform panelLineContainer,panelCorrectWordsContainer;

    List<LineManager> lines = new List<LineManager>();
    List<WordLineManager> correctWords= new List<WordLineManager>();

    [SerializeField]
    QuestionPanelScript questionPanel;

    P2DPanel myPanel;

    [HideInInspector]
    public CharacterButtonManager lastSelectedButton;
    string _selectedString;
    string selectedString
    {
        get { return _selectedString; }
        set
        {
            _selectedString = value;
            selectedStringText.text = value;
        }
    }

    bool _StartFindNewWord = false;
    public bool StartFindNewWord
    {
        get { return _StartFindNewWord; }
        set
        {
            _StartFindNewWord = value;
            if (_StartFindNewWord)
                selectedString = "";
            else
                lastSelectedButton = null;
        }
    }

    int wordGameSize;

    private void Awake()
    {
        myPanel = GetComponent<P2DPanel>();

    }

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < maxLines; i++)
        {
            lines.Add(Instantiate(LinePrefab, panelLineContainer));
            lines[i].Init(i);
        }
        for (int i = 0; i < maxWords; i++)
        {
            correctWords.Add(Instantiate(wordPrefab, panelCorrectWordsContainer));
        }

       
    }

    public void ShowPanel(Question question)
    {
        myPanel.Show();
        this.question = question;
        findedWords = 0;

        SetQuestion();
        SetCorrectWords();
    }
    public void Hide(bool immidiate = false)
    {
        myPanel.Hide(immidiate);
    }

    private void SetCorrectWords()
    {
        for (int i = 0; i < maxWords; i++)
        {
            if(i < question.words.Length)
            {
                correctWords[i].ShowLine(question.words[i]);
            }
            else
            {
                correctWords[i].Hide();
            }
        }
    }

    public void SetQuestion()
    {
        foreach (var item in lines)
        {
            item.HideLine();
        }
        wordGameSize = (int)question.wordGameSize;

        for (int i = 0; i < maxLines ; i++)
        {
            if( i < wordGameSize)
                lines[i].ShowEmptyLine(wordGameSize);
            else
                lines[i].HideLine();

        }

        for (int i = 0; i < question.words.Length; i++)
        {
            List<Vector2> currentWordPosition = GetWordPositions(i);
            for (int j = 0; j < currentWordPosition.Count; j++)
            {
                lines[(int)Mathf.Round(currentWordPosition[j].y)].ShowChar(question.words[i][j], (int)Mathf.Round(currentWordPosition[j].x), i);
            }
        }
    }

    private List<Vector2> GetWordPositions(int i)
    {
        switch (i)
        {
            case 0:
                return question.words1Positions;
            case 1:
                return question.words2Positions;
            case 2:
                return question.words3Positions;
            case 3:
                return question.words4Positions;
            case 4:
                return question.words5Positions;
            case 5:
                return question.words6Positions;
            case 6:
                return question.words7Positions;
            default:
                throw new Exception("Max words is 7");
        }
    }

    public void SetNextChar(CharacterButtonManager ch)
    {
        lastSelectedButton = ch;
        selectedString += ch.charText.text;
    }

    public void CheckWord()
    {
        int correctWordIndex = IsCorrectWord(selectedString);
        bool correctIndexes = true;
        if (correctWordIndex != -1)
        {
            for (int i = 0; i < wordGameSize; i++)
            {
                if (!lines[i].HasAllSelectedCorrectIndex(correctWordIndex))
                    correctIndexes = false;
            }

            if (correctIndexes)
            {
                findedWords++;
                correctWords[correctWordIndex].ShowCorrect();

                for (int i = 0; i < wordGameSize; i++)
                {
                    lines[i].RemoveSelected();
                }

                if(findedWords >= question.words.Length)
                {
                    StartCoroutine(questionPanel.VerifyAnswer(0, true));
                }
            }
            selectedString = "";

        }

        for (int i = 0; i < wordGameSize; i++)
        {
            lines[i].DeSelectLine();
        }
    }

    private int IsCorrectWord(string selectedString)
    {
        for (int i = 0; i < question.words.Length; i++)
        {
            if (question.words[i] == selectedString)
                return i;
        }

        return -1;
    }

    public void OnPointerDown(BaseEventData eventData)
    {
        StartFindNewWord = true;
    }

    public void OnPointerUp(BaseEventData eventData)
    {
        EndFindWord();
    }

    public void EndFindWord()
    {
        Instance.CheckWord();

        StartFindNewWord = false;

    }

}
