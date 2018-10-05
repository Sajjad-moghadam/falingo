using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WordLineManager : MonoBehaviour {

    const int maxCharacter = 10;

    string myWord;
    bool isFinded = false;

    [SerializeField]
    GameObject wordCharPrefab;

    WordCharacterScript[] characters = new WordCharacterScript[maxCharacter];
    private void Awake()
    {
        for (int i = 0; i < maxCharacter; i++)
        {
            characters[i] = Instantiate(wordCharPrefab,transform).GetComponent<WordCharacterScript>();
        }
    }


    public void ShowLine(string word)
    {
        gameObject.SetActive(true);
        isFinded = false;
        myWord = word;

        for (int i = 0; i < maxCharacter; i++)
        {
            if (i < myWord.Length)
                characters[i].ShowEmpty();
            else
                characters[i].HideCharacter();
        }
    }

    public void ShowCorrect()
    {
        isFinded = true;

        for (int i = 0; i < myWord.Length; i++)
        {
            characters[i].SetCharacter(myWord[i], (0.2f * i));
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }


   
	
}
