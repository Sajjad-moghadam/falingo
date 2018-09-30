using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanelScript : MonoBehaviour {

    P2DPanel myPanel;

    [SerializeField]
    Text headerText;

    private void Awake()
    {
        myPanel = GetComponent<P2DPanel>();
    }
   
    public void Show(string cat,string lesson)
    {
        myPanel.Show();

        //StartCoroutine(WriteByCharacter(cat + "\n" + lesson));
        StartCoroutine(ShowAndRead(cat,lesson));
    }

    IEnumerator ShowAndRead(string cat, string lesson)
    {
        headerText.text = cat + "\n" + lesson;

        yield return new WaitForSeconds(0.6f);

        Setting.Speak(cat);

        yield return new WaitForSeconds(0.25f);

        Setting.Speak(lesson,0.7f);

        yield return new WaitForSeconds(1f);

        myPanel.Hide();

    }

    WaitForSeconds writeSpeed = new WaitForSeconds(0.03f);
    IEnumerator WriteByCharacter(string text)
    {
        int index = 0;
        while(index < text.Length)
        {
            headerText.text = text.Substring(0, index);

            yield return writeSpeed;
        }
    }

}
