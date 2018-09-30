using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExamButtonScript : MonoBehaviour {

    public int examCategorys = 6,examQuestions = 30;
    public string examTitle = "Biginner";

    [SerializeField]
    Text examScoreText;

    int score;
    int maxScore;
    bool isOpen = false;

	// Use this for initialization
	void Start ()
    {
        maxScore = examCategorys * 100 ;
        GameMng.onScoreChangeEvent += CheckScoreChange;

        CalculateGainedScore();
	}


    void CalculateGainedScore()
    {
        score = 0;

        for (int i = 0; i < examCategorys; i++)
        {
            score += GameMng.GetCategoryScore((QuestionType)i);
        }

        if (score >= MinScore2Open())
            isOpen = true;

        examScoreText.text = score.ToString() + " / " + MinScore2Open().ToString();
    }

    public void OnClick()
    {
        if(score >= MinScore2Open())
        {
            GameMng.selectedExam = this;
            GameMng.Instance.ShowExamPanel();

        }
        else
        {
            Setting.MessegeBox.SetMessege("برای ورود به امتحان باید حداقل " + MinScore2Open() + " امتیاز از مراحل قبل بدست بیاری.","","امتیازت کافی نیست");
        }
    }

    int MinScore2Open()
    {
        return (maxScore / 2);
    }

    public void CheckScoreChange(QuestionType type)
    {
        if((int)type < examCategorys)
        {
            bool wasOpen = isOpen;
            CalculateGainedScore();

            if(!wasOpen && isOpen)
            {
                Setting.MessegeBox.SetMessege("امتحان سطح جدید باز کردی", "", "یوهووو");
                //TODO:Sound
            }
        }
    }
}
