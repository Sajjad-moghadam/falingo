using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionButtonScript : MonoBehaviour {

    Color GreenColor = new Color(0, 1, 0, 0.5f);
    Color RedColor = new Color(1, 0, 0, 0.5f);

    Text buttonText;
    Image buttonImage,coverImage;
    Toggle toggle;

    private void Awake()
    {
        Transform temp;
        temp = transform.Find("Text");
        if (temp != null)
            buttonText = temp.GetComponent<Text>();
        else
            Debug.LogError("No Text found for button");

        temp = transform.Find("Toggle");
        if (temp != null)
            toggle = temp.GetComponent<Toggle>();
        else
            Debug.LogError("No Toggle found for button");

        temp = transform.Find("ImageCover");
        if (temp != null)
            coverImage = temp.GetComponent<Image>();
        else
            Debug.LogError("No ImageCover found for button");

        temp = transform.Find("Image");
        if(temp != null)
            buttonImage = temp.GetComponent<Image>();
    }


    public void SetButton(string text,Sprite image = null)
    {
        gameObject.SetActive(true);
        toggle.isOn = false;
        coverImage.gameObject.SetActive(false);
        buttonText.text = text.faConvert();
        if(image != null)
        {
            if(buttonImage != null)
            {
                buttonImage.sprite = image;
            }
            else
            {
                Debug.LogError("Image component not found!!!");
            }
        }
    }

    public void SetGreenCover()
    {
        coverImage.gameObject.SetActive(true);
        //coverImage.CrossFadeColor(GreenColor, 0.2f,true,true);
        coverImage.color = GreenColor;
    }

    public void SetRedCover()
    {
        coverImage.gameObject.SetActive(true);
        //coverImage.CrossFadeColor(RedColor, 0.2f, true, true);
        coverImage.color = RedColor;
    }
}
