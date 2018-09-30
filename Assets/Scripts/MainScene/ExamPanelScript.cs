using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExamPanelScript : MonoBehaviour {

    P2DPanel myPanel;

    [SerializeField]
    Text bestScoreText;

    [SerializeField]
    Animator stampAnimator;


	// Use this for initialization
	void Awake ()
    {

        myPanel = GetComponent<P2DPanel>();
	}


    public void Show()
    {
        myPanel.Show();

        int bestScore = GameMng.GetExamBestScore(GameMng.selectedExam.name);
        int maxScore = GameMng.selectedExam.examQuestions * 3;

        bestScoreText.text = bestScore + "/" + maxScore;

        if(bestScore >= maxScore/2f)
        {
            StartCoroutine(ShowStamp());
        }
        else
        {
            stampAnimator.gameObject.SetActive(false);

        }
    }

    IEnumerator ShowStamp()
    {
        yield return new WaitForSeconds(1.5f);

        //TODO:Sound
        stampAnimator.gameObject.SetActive(true);
        stampAnimator.SetTrigger("StampTrigger");
    }

    public void ExamRequestClick()
    {
        Setting.MessegeBox.SetMessege("برای شروع امتحان 50 الماس باید استفاده کنی","","آماده ای");
        Setting.MessegeBox.OnOkButtonClickEvent += MessegeBox_OnOkButtonClickEvent;
    }

    private void MessegeBox_OnOkButtonClickEvent()
    {
        myPanel.Hide();
        GameMng.Instance.ShowExam();
    }
}
