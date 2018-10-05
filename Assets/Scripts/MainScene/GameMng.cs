using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using Backtory.Core.Public;

public enum QuestionMode { Easy, Intermed, Diffy }
public enum QuestionType { Animals, Actions, Colors, Food, Fruits, BodyParts, Weather, Toys, Sports, Clothes, Jobs, Transport, SchoolThings, Objects }
public enum QuestionStruct { Choice, Pic, WordGame }

public delegate void OnScoreChange(QuestionType type);

public class GameMng : SingletonMahsa<GameMng>
{
    public const int lastCategoryIndex = 13;
    public static QuestionType selectedCategory;
    public static int selectedLessonIndex;
    public static ExamButtonScript selectedExam;

    public static event OnScoreChange onScoreChangeEvent;

    [SerializeField]
    List<GameObject> mainPanels;

    [SerializeField]
    List<Category> categorys;

    [SerializeField]
    List<ExamButtonScript> exams;

    [SerializeField]
    P2DAmountShower xpShower;

    [SerializeField]
    ExamPanelScript examPanel;

    private float delay = 1;

    public LessonPanelScript LessonPanel;
    public QuestionPanelScript questionPanel;

    private void Awake()
    {
        if (GetLastOpenLesson(0) == 0)
        {
            SetLastOpenLesson(0, 1);
        }
    }

    void Start()
    {
        Setting.initSetting();

        SetMainPanel(0);


        onScoreChangeEvent += GameMng_onScoreChangeEvent;

        UpdateXpShower();

    }

    private void GameMng_onScoreChangeEvent(QuestionType type)
    {
        UpdateXpShower();
    }

    private void UpdateXpShower()
    {
        int currentScore = 0;
        int maxScore = (lastCategoryIndex + 1) * 300;

        for (int i = 0; i <= lastCategoryIndex; i++)
        {
            currentScore += GetCategoryScore((QuestionType)i);
        }

        foreach (var item in exams)
        {
            maxScore += (item.examQuestions * 3);
            currentScore += GetExamBestScore(item.name);
        }

        xpShower.SetMaxAmount(maxScore);
        xpShower.SetAmount(currentScore);



        SendScore(currentScore);

    }

    public void SetMainPanel(int panelIndex)
    {
        foreach (var item in mainPanels)
        {
            item.SetActive(false);
        }

        mainPanels[panelIndex].SetActive(true);

    }

    public void ShowLessonPanel(QuestionType category)
    {
        selectedCategory = category;
        LessonPanel.Show();
    }

    public void ShowQuestionPanel()
    {
        questionPanel.Show();
    }

    public void ShowExamPanel()
    {
        examPanel.Show();
    }

    public void ShowExam()
    {
        questionPanel.Show(QType.Exam);
    }

    public Category GetCategory(QuestionType categoryType)
    {
        return categorys.First(s => s.questionType == categoryType);
    }


    const string lessonOfLevelKey = "lessonOfLevelKey";

    public static int GetLastOpenLesson(QuestionType category)
    {
        int lesson = 0;
        P2DSecurety.SecureLocalLoad(lessonOfLevelKey + category.ToString(), out lesson);
        return lesson;
    }

    public static void SetLastOpenLesson(QuestionType category, int value)
    {
        P2DSecurety.SecureLocalSave(lessonOfLevelKey + category.ToString(), value);

        if (onScoreChangeEvent != null)
        {
            onScoreChangeEvent(category);
        }
    }


    public static int GetLessonBestScore(QuestionType Category, int lessonNumber)
    {
        int lesson = 0;
        P2DSecurety.SecureLocalLoad(lessonOfLevelKey + Category.ToString() + lessonNumber.ToString(), out lesson);
        return lesson;
    }

    public static void SetLessonBestScore(QuestionType Category, int lessonNumber, int value)
    {
        P2DSecurety.SecureLocalSave(lessonOfLevelKey + Category.ToString() + lessonNumber.ToString(), value);

        if (onScoreChangeEvent != null)
        {
            onScoreChangeEvent(Category);
        }
    }

    public static int GetExamBestScore(string examName)
    {
        int score = 0;
        P2DSecurety.SecureLocalLoad(lessonOfLevelKey + examName, out score);
        return score;
    }

    public static void SetExamBestScore(string examName, int value)
    {
        P2DSecurety.SecureLocalSave(lessonOfLevelKey + examName, value);

        if (onScoreChangeEvent != null)
        {
            onScoreChangeEvent((QuestionType)lastCategoryIndex);
        }
    }

    public static int GetCategoryScore(QuestionType questionType)
    {
        int score = 0;
        for (int i = 1; i <= 3; i++)
        {
            score += GetLessonBestScore(questionType, i);
        }

        if (score == 90)
            score = 100;

        return score;
    }

    public static string GetLessonNumberString()
    {
        switch (selectedLessonIndex)
        {
            case 1:
                return "one";
            case 2:
                return "two";
            case 3:
                return "three";
            default:
                return "one";
        }
    }

    public void SendScore(int score)
    {
        try
        {
            // Step 1: Creating parameters for GameOver event
            List<BacktoryGameEvent.FieldValue> fieldValues = new List<BacktoryGameEvent.FieldValue>()
             {
                 new BacktoryGameEvent.FieldValue("Score", score),
            };

            // Step 2: Creating GameOver event and filling its data
            BacktoryGameEvent backtoryGameEvent = new BacktoryGameEvent()
            {
                Name = "ScoreChange",
                FieldsAndValues = fieldValues
            };

            // Step 3: Sending event to server
            backtoryGameEvent.SendInBackground(backtoryResponse =>
            {
                // Checking callback from server
                if (backtoryResponse.Successful)
                {
                    Debug.Log("saved event successfully");
                }
                else
                {
                    Debug.Log(backtoryResponse.Message);
                    // do something based on BactoryResponse.Code
                }
            });
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }

    }


}
