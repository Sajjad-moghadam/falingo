using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExamPanelScript : MonoBehaviour {

    P2DPanel myPanel;

    [SerializeField]
    Text bestScoreText,diamondRequestedText;

    [SerializeField]
    Animator stampAnimator;

    [SerializeField]
    Button CertificateButton;

    [SerializeField]
    CertificatePanel certificatePanel;

	// Use this for initialization
	void Awake ()
    {

        myPanel = GetComponent<P2DPanel>();
	}


    public void Show()
    {
        myPanel.Show();

        int bestScore = GameMng.GetExamBestScore(GameMng.selectedExam.examTitle);
        int maxScore = GameMng.selectedExam.examQuestions * 3;

        bestScoreText.text = bestScore + "/" + maxScore;
        diamondRequestedText.text = GameMng.selectedExam.examDiamondPrice.ToString();

        if (bestScore >= maxScore/2f)
        {
            StartCoroutine(ShowStamp());
            CertificateButton.gameObject.SetActive(true);
        }
        else
        {
            CertificateButton.gameObject.SetActive(true);
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
        Setting.MessegeBox.SetMessege("برای شروع امتحان " + GameMng.selectedExam.examDiamondPrice + " الماس باید استفاده کنی ", "","آماده ای");
        Setting.MessegeBox.OnOkButtonClickEvent += MessegeBox_OnOkButtonClickEvent;
    }

    private void MessegeBox_OnOkButtonClickEvent()
    {
        if (GameMng.GetDiamondNumber() >= GameMng.selectedExam.examDiamondPrice)
        {
            GameMng.Instance.RemoveDiamond(GameMng.selectedExam.examDiamondPrice);
            myPanel.Hide();
            GameMng.Instance.ShowExam();
        }
        else
        {
            Setting.MessegeBox.SetMessege("الماس کافی نداری");
        }
          
    }

    public void ShowCertificatePanel()
    {
        string name = "شما";
        if (Setting.authResponse != null && Setting.authResponse.DisplayName != null)
            name = Setting.authResponse.DisplayName;

        certificatePanel.Show(bestScoreText.text, name, GameMng.selectedExam.examTitle, GameMng.selectedExam.examDegree);
    }
}
