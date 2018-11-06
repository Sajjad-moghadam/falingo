using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(P2DPanel))]
public class AchivmentPanelScript : MonoBehaviour {

    [SerializeField]
    Text text;
    [SerializeField]
    Image image;

    P2DPanel myPanel;

    [SerializeField]
    Button shareButton, BackButton;

    private void Awake()
    {
        myPanel = GetComponent<P2DPanel>();
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	

    public void Show(string message,Sprite icon)
    {
        myPanel.Show();

        text.text = message;
        image.sprite = icon;
    }

    public void ShareClick()
    {

        StartCoroutine(TakeAchivmentPicture());

    }

    IEnumerator TakeAchivmentPicture()
    {

        CoroutineWithData takeImageRoutine = new CoroutineWithData(this, GameMng.TakeImage());

        shareButton.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);

        yield return takeImageRoutine.coroutine;

        shareButton.gameObject.SetActive(true);
        BackButton.gameObject.SetActive(true);

        Texture2D texturePic = (Texture2D)takeImageRoutine.result;

        //image.sprite = Sprite.Create(texturePic, new Rect(0,0,texturePic.width, texturePic.height),Vector2.zero);

        GameMng.ShareImage(texturePic);
    }
}
