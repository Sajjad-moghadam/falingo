using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public delegate void OnOkButtonClick();
public delegate void OnCancelButtonClick();

[RequireComponent(typeof(P2DPanel))]
public class P2DMessegeBox : MonoBehaviour {

    public event OnOkButtonClick OnOkButtonClickEvent;
    public event OnCancelButtonClick OnCancelButtonClickEvent;

    [SerializeField]
    Button okButton;

    [SerializeField]
    Button cancelButton;

    [SerializeField]
    Text messegeHeader;

    [SerializeField]
    Text messege;
   
    [SerializeField]
    Text messegeSpecial;

    [SerializeField]
    Image raycastBlockerImage;

    [SerializeField]
    P2DPanel myPanel;

	// Use this for initialization
	void Start () {

	}
	

    /// <summary>
    ///  **** it will remove all old Event
    /// </summary>
    /// <param name="line1"></param>
    /// <param name="line2"></param>
    /// <param name="specialNote"></param>
    public void SetMessege(string message,string specialNote = "",string header ="",bool raycastBlock = true)
    {
        OnOkButtonClickEvent = null;
        OnCancelButtonClickEvent = null;
        SetRayCastBlocker(raycastBlock);

        messege.text = message;
        messegeSpecial.text = specialNote;
        messegeHeader.text = header;

        myPanel.Show();
    }

    public void OnCancelButtonClick()
    {

        if (OnCancelButtonClickEvent != null)
        {
            OnCancelButtonClickEvent();
        }

        GetComponent<P2DPanel>().Hide();

    }

    public void OkButtonClick()
    {

        if (OnOkButtonClickEvent != null)
        {
            OnOkButtonClickEvent();
        }

        GetComponent<P2DPanel>().Hide();

    }

    public void SetRayCastBlocker(bool value)
    {
        raycastBlockerImage.raycastTarget = value;
    }

}

