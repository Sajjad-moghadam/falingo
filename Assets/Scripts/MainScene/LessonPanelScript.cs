using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LessonPanelScript : MonoBehaviour {

    P2DPanel myPanel;

    [SerializeField]
    Text lessonScore;
    [SerializeField]
    Text lessonNumber;
    [SerializeField]
    Button nextLesson;
    [SerializeField]
    Button prevLesson;

    // Use this for initialization
    void Start () {
        myPanel = GetComponent<P2DPanel>();
	}
	
	public void Show()
    {
        myPanel.Show();
        GameMng.selectedLessonIndex = GameMng.GetLastOpenLesson(GameMng.selectedCategory);
        UpdatePanel();
    }

    private void UpdatePanel()
    {
        nextLesson.interactable = true;
        prevLesson.interactable = true;

        if (GameMng.selectedLessonIndex == 1)
            prevLesson.interactable = false;

        if (GameMng.selectedLessonIndex == GameMng.GetLastOpenLesson(GameMng.selectedCategory))
            nextLesson.interactable = false;

        lessonNumber.text = 3 + " / " + GameMng.selectedLessonIndex.ToString();
        lessonScore.text = GameMng.GetLessonBestScore(GameMng.selectedCategory, GameMng.selectedLessonIndex) + "/" + 33;
    }

    public void StartLesson()
    {
        myPanel.Hide();
        GameMng.Instance.ShowQuestionPanel();
    }

    public void NextLessonClick()
    {
        GameMng.selectedLessonIndex++;
        UpdatePanel();
    }

    public void PrevLessonClick()
    {
        GameMng.selectedLessonIndex--;
        UpdatePanel();
    }
}
