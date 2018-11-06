using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ResultType
{
    Awful = 0,
    Bad = 1,
    NotGood = 2,
    NotBad = 3,
    Normal = 4 ,
    Good = 5,
    VeryGood = 6 ,
    Excellent = 7
}

public class ResultPanelScript : MonoBehaviour {

    P2DPanel myPanel;

    [SerializeField]
    QuestionPanelScript questionPanel;

    [SerializeField]
    Text titleText, scoreText, bestScoreText;

    [SerializeField]
    Button homeButton, restartButton, nextButton;

    [SerializeField]
    Animator stampAnimator;

	// Use this for initialization
	void Start () {
        myPanel = GetComponent<P2DPanel>();
	}

    public void Show(string score, string bestScore, ResultType type, bool canRestart)
    {
        myPanel.Show();
        titleText.text = GetTitleFromType(type);
        scoreText.text = score;
        bestScoreText.text = bestScore;

        if (type >= ResultType.NotBad)
        {
            StartCoroutine(ShowStamp());
            nextButton.interactable = true;

        }
        else
        {
            stampAnimator.gameObject.SetActive(false);
            nextButton.interactable = false;
        }

        if (canRestart)
        {
            restartButton.interactable = true;
        }
        else
        {
            restartButton.interactable = false;

        }
    }

    IEnumerator ShowStamp()
    {
        yield return new WaitForSeconds(1);

        //TODO:Sound
        stampAnimator.gameObject.SetActive(true);
        stampAnimator.SetTrigger("StampTrigger");
    }

    private string GetTitleFromType(ResultType type)
    {
        switch (type)
        {
            case ResultType.Awful:
                return "خیلی بد بود";
            case ResultType.Bad:
                return "بد بود";
            case ResultType.NotGood:
                return "خوب نبود";
            case ResultType.NotBad:
                return "بد نبود";
            case ResultType.Normal:
                return "معمولی جواب دادی";
            case ResultType.Good:
                return "خوب بودی";
            case ResultType.VeryGood:
                return "خیلی خوب بودی";
            case ResultType.Excellent:
                return "عالی بودی";
            default:
                return "خوب بود";

        }

    }

    public void HomeClick()
    {
        myPanel.Hide();
        questionPanel.Hide();
    }

    public void RestartClick()
    {
        myPanel.Hide();
        questionPanel.Restart();
    }

    public void NextClick()
    {
        myPanel.Hide();
        questionPanel.Go2NextLesson();
    }
}
