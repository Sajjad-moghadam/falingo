using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup), typeof(Animator))]
public class P2DPopUpNotification : MonoBehaviour {

    Animator animator;

    [SerializeField]
    Text text;

    [SerializeField]
    Image image;

    [SerializeField]
    AudioClip defaultAudio;

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    // Use this for initialization
    void Start ()
    {

        if (text == null)
            Debug.LogError("P2DPopUpNotification text is null");
        if (image == null)
            Debug.LogError("P2DPopUpNotification image is null");
        if (defaultAudio == null)
            Debug.LogError("P2DPopUpNotification defaultAudio is null");

    }

    public void Show(string Messege,Sprite sprite = null,AudioClip notifClip = null)
    {
        if (image != null)
        {
            image.gameObject.SetActive(true);
            image.sprite = sprite;
        }
        else
        {
            image.gameObject.SetActive(false);

        }
        if(notifClip != null)
        {
            Setting.AudioPlayer.PlayOneShot(notifClip);
        }
        else
        {
            Setting.AudioPlayer.PlayOneShot(defaultAudio);
        }

        text.text = Messege;
        animator.SetTrigger("In");
    }
}
