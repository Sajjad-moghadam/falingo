using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum WaitingIcon
{
    WIFI,
    Soot
}

[RequireComponent(typeof(P2DPanel))]
public class WaitingPanelScriptP2D : MonoBehaviour
{

    [SerializeField]
    Text messageText;
    [SerializeField]
    Image image,imageRayBlocker;

    [SerializeField]
    Sprite wifiSprite, sootSprite, wifiSpriteIrancell, sootSpriteIrancell;

    P2DPanel myPanel;

    WaitForSeconds wait4Hide = new WaitForSeconds(30);

    static string defaultMessage = "در حال ارتباط با سرور".faConvert();

    void Awake()
    {
        myPanel = GetComponent<P2DPanel>();
    }

    public void Show(string message = "", WaitingIcon iconType = WaitingIcon.WIFI,bool blockRayCast = true)
    {
        myPanel.Show();

        if (blockRayCast)
            imageRayBlocker.raycastTarget = true;
        else
            imageRayBlocker.raycastTarget = false;


        if (message == "")
            messageText.text = defaultMessage;
        else
            messageText.text = message;

        StartCoroutine(HideHandler());
    }

    public void Hide()
    {
        StopAllCoroutines();
        myPanel.Hide();
    }

    /// <summary>
    /// Hide panel if it take more than 30 secound
    /// </summary>
    /// <returns></returns>
    IEnumerator HideHandler()
    {
        yield return wait4Hide;
        SceneManager.LoadScene(Setting.introScene);
    }

}
