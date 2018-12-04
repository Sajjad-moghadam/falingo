using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Serialization;
//using Backtory.Core.Public;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using System.Text.RegularExpressions;
using System.Net;
using UnityEngine.Networking;
using GameSparks.Api.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

public class Setting
{


    private static string telegramChannelUrl = "https://telegram.me/AsurikGame";
    private static string otherProductionURL = "https://cafebazaar.ir/developer/phoenix2d/";
    //public static string mciURL = "http://roopayi.sadrcom.com/api/account/";

    public static string[] tips =
    {
        "Practice to remember",

    };

    public static AuthenticationResponse authResponse;
    public static AudioSource AudioPlayer;
    public static P2DMessegeBox MessegeBox;
    public static P2DNotification notificationMessage;
    public static P2DPopUpNotification popupNotification;
    public static WaitingPanelScriptP2D waitingPanel;
    public static P2DCollectManager collectManager;
    private static TextToSpeech text2Speach;
    public static List<ScoreHistory> historyList = new List<ScoreHistory>();

    public static string introScene = "Intro";
    public static string mainScene = "MainApp";

    public static string sexKey = "SexKey";
    public static string ageKey = "AgeKey";




    public static bool isSettingInit = false;

    public static void initSetting()
    {
        if (!isSettingInit)
        {
            InitP2DEngine();
            LockManager.InitLocks();

            InitMute();
            isSettingInit = true;
        }
        else
        {
            Log("Setting alredy initialized");
        }
    }

    public static bool Mute
    {
        get
        {
            int val = PlayerPrefs.GetInt("Mute");
            if (val != 0)
                return true;
            else
                return false;
        }
        set
        {

            if (value)
            {
                PlayerPrefs.SetInt("Mute", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Mute", 0);
            }

            InitMute();
        }
    }


    public static bool MusicMute
    {
        get
        {
            int val = PlayerPrefs.GetInt("MusicMute");
            if (val != 0)
                return true;
            else
                return false;
        }
        set
        {

            if (value)
            {
                PlayerPrefs.SetInt("MusicMute", 1);
            }
            else
            {
                PlayerPrefs.SetInt("MusicMute", 0);
            }
        }
    }

    static void InitMute()
    {
        AudioListener.volume = Mute ? 0 : 1;

    }

    private static void InitP2DEngine()
    {
        GameObject canvasP2D;
        if ((canvasP2D = GameObject.Find("CanvasP2D")) == null)
        {
            canvasP2D = GameObject.Instantiate(Resources.Load<GameObject>("CanvasP2D"));

        }
        GameObject.DontDestroyOnLoad(canvasP2D);
        AudioPlayer = canvasP2D.transform.Find("AudioSource").GetComponent<AudioSource>();
        MessegeBox = canvasP2D.transform.Find("MessegeBoxPanel").GetComponent<P2DMessegeBox>();
        notificationMessage = canvasP2D.transform.Find("NotificationText").GetComponent<P2DNotification>();
        popupNotification = canvasP2D.transform.Find("PopUpNotification").GetComponent<P2DPopUpNotification>();
        waitingPanel = canvasP2D.transform.Find("PanelWaiting").GetComponent<WaitingPanelScriptP2D>();
        collectManager = canvasP2D.transform.Find("PanelCollectManager").GetComponent<P2DCollectManager>();
        text2Speach = canvasP2D.transform.Find("Text2Speach").GetComponent<TextToSpeech>();
    }


    const string levelKey = "levelKey";
    public static int lastLevel
    {
        get
        {
            int level = 0;
            P2DSecurety.SecureLocalLoad(levelKey, out level);
            return level;
        }
        set
        {
            P2DSecurety.SecureLocalSave(levelKey, value);
        }
    }

   
    public static void ShowSoonNotification()
    {
        notificationMessage.Show("به زودی".faConvert());
    }

    //const string subscribeToken = "tokenKey";
    //public static string GetToken()
    //{
    //    string token = "";
    //    P2DSecurety.SecureLocalLoad(subscribeToken, out token);

    //    return token;
    //}

    const string submitKey = "submetKey";

    public static IEnumerator SubmitRate()
    {
        PlayerPrefs.SetInt(submitKey, 1);

        try
        {

            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");

            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_EDIT"));
            intentObject.Call<AndroidJavaObject>("setData", uriClass.CallStatic<AndroidJavaObject>("parse", "market://details?id=com.Phoenix2D.gelevele"));
            intentObject.Call<AndroidJavaObject>("setPackage", "com.farsitel.bazaar");

            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            currentActivity.Call("startActivity", intentObject);
        }
        catch
        {

        }
        yield return new WaitForSeconds(0.1f);

    }

    public static void MuteSound(bool val)
    {
        Mute = val;
    }


    public static void Log(string logString)
    {
#if UNITY_EDITOR
        Debug.Log(logString);
#endif
    }

    public static int joinedTelegramChannel
    {
        get { return PlayerPrefs.GetInt("telegramSeen"); }
        set
        {
            PlayerPrefs.SetInt("telegramSeen", value);
        }
    }
    public static IEnumerator OpenTelegramChanel()
    {
        joinedTelegramChannel = 1;
        Application.OpenURL(telegramChannelUrl);
        yield return null;
    }

    public static IEnumerator OpenOtherProductionLink()
    {
        Application.OpenURL(otherProductionURL);
        yield return null;
    }


    public static IEnumerator SendRequest(string url, WWWForm parameters = null, Dictionary<string, string> headers = null, Action<bool, string, WWW> callBack = null)
    {
        if (!string.IsNullOrEmpty(url))
        {
            byte[] rawData = null;
            if (parameters != null)
            {
                rawData = parameters.data;
            }
            using (var request = new WWW(url, rawData, headers))
            {
                yield return request;
                if (string.IsNullOrEmpty(request.error))
                {// ok
                    if (callBack != null) callBack(true, request.text, request);
                }
                else
                {// error
                    if (callBack != null) callBack(false, request.error, request);
                }
                // kill request after work is done.
                request.Dispose();
            }
        }
        else
        {// url is null
            if (callBack != null) callBack(false, "url is null!", null);
        }
    }


    //public static JsonSerializerSettings JsonnetSetting()
    //{
    //    return new JsonSerializerSettings()
    //    {
    //        MissingMemberHandling = MissingMemberHandling.Ignore,
    //        NullValueHandling = NullValueHandling.Ignore,
    //        DefaultValueHandling = DefaultValueHandling.Include,
    //        ContractResolver = new CamelCasePropertyNamesContractResolver(),
    //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    //    };
    //}

    public static bool IsPhoneNumber(string number)
    {
        return Regex.Match(number, @"\(?\d{3}\)?[-\.]? *\d{3}[-\.]? *[-\.]?\d{4}").Success;
    }

    public static void Speak(string text,float speed = 0.9f,bool showToast = false)
    {
        try
        {
            text2Speach.SetSpeed(speed);
            text2Speach.Speak(text, (string msg) =>
            {
                if (showToast)
                    text2Speach.ShowToast(msg);
            });
        }
        catch (Exception e)
        {

        }
      
    }

    public static JsonSerializerSettings JsonnetSetting()
    {
        return new JsonSerializerSettings()
        {
            MissingMemberHandling = MissingMemberHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Include,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented
        };
    }

    public static IEnumerator GetHistoryData()
    {
        string url = string.Format("http://sajjadcv.ir/lingoland/getScores.php?user_id='{0}'", SystemInfo.deviceUniqueIdentifier);
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.LogError("Error While Sending: " + uwr.error);
        }
        else
        {
             historyList = JsonConvert.DeserializeObject<List<ScoreHistory>>(uwr.downloadHandler.text, JsonnetSetting());

        }
    }

    public static List<ScoreHistory> GetHistory(string categoryName,int lessonNumber)
    {
        List<ScoreHistory> lessonHistory = new List<ScoreHistory>();

        foreach (var item in historyList)
        {
            if(item.lesson == lessonNumber && item.category == categoryName)
            {
                lessonHistory.Add(item);
            }
        }

        return lessonHistory;
    }

    public static IEnumerator SendData(string uri, List<IMultipartFormSection> formData = null, Action<UnityWebRequest> callBack = null)
    {

        UnityWebRequest uwr = UnityWebRequest.Post(uri, formData);
        uwr.method = "POST";
        uwr.chunkedTransfer = false;////ADD THIS LINE
        yield return uwr.SendWebRequest();

        if (callBack != null)
            callBack(uwr);

        if (uwr.isNetworkError)
        {
            Debug.LogError("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }

    }

}
