using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGameTestScript : MonoBehaviour {

    [SerializeField]
    Question question;

    [SerializeField]
    WordsGameManager wordGameManager;

	// Use this for initialization
	void Start ()
    {
        Setting.initSetting();
        StartCoroutine(startGame());
	}

    IEnumerator startGame()
    {
        yield return new WaitForEndOfFrame();

        wordGameManager.ShowPanel(question);

    }

}
