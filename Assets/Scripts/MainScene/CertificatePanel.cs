using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(P2DPanel))]
public class CertificatePanel : MonoBehaviour {

    P2DPanel myPanel;

    [SerializeField]
    List<Sprite> sprites;

    [SerializeField]
    Image backImage;

    [SerializeField]
    Text resultText,messageText;

    [SerializeField]
    Button shareButton, BackButton;

    [SerializeField]
    AudioClip TadaClip;

    // Use this for initialization
    void Start () {
       myPanel = GetComponent<P2DPanel>();
	}
	
	public void Show(string result,string name,string course,int degree)
    {
        myPanel.Show();
        Setting.AudioPlayer.PlayOneShot(TadaClip);
        backImage.sprite = sprites[degree%sprites.Count];
        resultText.text = result;

        messageText.text = name + " با موفقیت دوره " + course + " را به پایان رساند.";
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
