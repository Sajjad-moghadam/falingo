using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButtonScript : MonoBehaviour {

    [SerializeField]
    int categoryIndex;

    [SerializeField]
    Text titleText,totalScoreText;

    [SerializeField]
    Image flagImage;

    Button myButton;

    Category myCategory;

	// Use this for initialization
	void Start ()
    {
        myCategory = GameMng.Instance.GetCategory((QuestionType)categoryIndex);
        GameMng.onScoreChangeEvent += CheckScoreChange;
        myButton = GetComponent<Button>();
        GetComponent<Image>().sprite = myCategory.buttonSprite;
        titleText.text = myCategory.questionType.ToString();
        CheckIsOpen();

        CalculateScore();

    }

    private void CheckIsOpen()
    {
        if (GameMng.GetLastOpenLesson(myCategory.questionType) == 0)
        {
            myButton.interactable = false;
        }
        else
            myButton.interactable = true;
    }

    public void CalculateScore()
    {
        int score = GameMng.GetCategoryScore(myCategory.questionType);

        totalScoreText.text = score + "/" + "100";

        if(score == 100)
        {
            flagImage.gameObject.SetActive(true);
        }
        else
        {
            flagImage.gameObject.SetActive(false);
        }

    }

    public void OnClick()
    {
        GameMng.Instance.ShowLessonPanel(myCategory.questionType);
    }

    private void CheckScoreChange(QuestionType type)
    {
        if(myCategory.questionType == type)
        {
            CheckIsOpen();
            CalculateScore();
        }
       
    }
	
}
