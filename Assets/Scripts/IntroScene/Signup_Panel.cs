using System;
using Backtory.Core.Public;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using Backtory.InAppPurchase.Public;
using System.Text.RegularExpressions;
using System.Net.Mail;
using UnityEngine.SceneManagement;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using GameSparks.Core;

public class Signup_Panel : MonoBehaviour
{

    const string usernameKey = "userKey";
    //const string emailKey = "emailKey";
    //const string passKey = "passKey";
    const string alreadyRegistered = "alreadyRegisteredKey";

    // Signup Panel
    public InputField usernameInputreg;
    public Dropdown ageDropDown;
    public Dropdown sexDropDown;

    // Login Panel
    public InputField usernameInputlog;

    public void Start()
    {
        Setting.initSetting();

#if UNITY_EDITOR
        //PlayerPrefs.DeleteAll(); //danger*****
#endif
        // load local
        if (PlayerPrefs.GetInt(alreadyRegistered) == 1)
        {
            //string savedusername = PlayerPrefs.GetString(usernameKey);
            //string savedpass = PlayerPrefs.GetString(passKey);
            LoginProcess();
        }

        //var request = new LoginWithAndroidDeviceIDRequest();
        //request.AndroidDeviceId = SystemInfo.deviceUniqueIdentifier;
        //PlayFabClientAPI.LoginWithAndroidDeviceID(request, PlayfabLoginCallback, PlayfabLoginCallbackError, "Sajjad");

        //var signupRequest = new RegisterPlayFabUserRequest();
        //signupRequest.
        //PlayFabClientAPI.RegisterPlayFabUser()

        //GSRequestData sd = new GSRequestData().AddNumber(Setting.sexKey, 1).AddNumber(Setting.ageKey, 1);
        //new RegistrationRequest().SetUserName(SystemInfo.deviceUniqueIdentifier).SetPassword(SystemInfo.deviceUniqueIdentifier).SetDisplayName("سجاد").SetScriptData(sd).Send((RegistrationResponse response) =>
        //{
        //    if (response.HasErrors)
        //    {

        //        Setting.MessegeBox.SetMessege("خطا در ایجاد پروفایل کاربری");

        //    }
        //    else
        //    {
        //        PlayerPrefs.SetInt(alreadyRegistered, 1);
        //        //It worked!!
        //    }
        //});

       

    }

    void RegisterCallBack(AuthenticationResponse response)
    {

    }


    //void PlayfabLoginCallback(LoginResult result)
    //{
    //    Debug.Log("YESSSSSS");
    //}
    //void PlayfabLoginCallbackError(PlayFabError error)
    //{
    //    Debug.LogError(error.GenerateErrorReport());
    //}

    public void onRegisterClick()
    {

        if ((usernameInputreg.text != ""))
        {
            Setting.waitingPanel.Show("در حال ثبت نام");


            GSRequestData sd = new GSRequestData().AddNumber(Setting.sexKey, sexDropDown.value).AddNumber(Setting.ageKey, ageDropDown.value);
            new RegistrationRequest().SetUserName(SystemInfo.deviceUniqueIdentifier).SetPassword(SystemInfo.deviceUniqueIdentifier).SetDisplayName(usernameInputreg.text).SetScriptData(sd).Send((RegistrationResponse response) =>
            {
                Setting.waitingPanel.Hide();

                if (response.HasErrors)
                {
                    Debug.LogError(response.Errors.BaseData["DETAILS"].ToString());
                    Setting.MessegeBox.SetMessege("خطا در ایجاد پروفایل کاربری");

                }
                else
                {
                    PlayerPrefs.SetInt(alreadyRegistered, 1);
                    LoginProcess();
                }
            });
  
        }
        else if ((usernameInputreg.text == ""))
        {

            Setting.MessegeBox.SetMessege("لطفا نام کاربری خود را وارد کنید.");
        }
    }

    public void LoginProcess()
    {
        Setting.waitingPanel.Show("در حال ورود");

        new AuthenticationRequest().SetUserName(SystemInfo.deviceUniqueIdentifier).SetPassword(SystemInfo.deviceUniqueIdentifier).Send((AuthenticationResponse response) =>
        {
            Setting.waitingPanel.Hide();

            if (response.HasErrors)
            {
                if(response.Errors.BaseData["DETAILS"].ToString().Contains("UNRECOGNISED"))
                    Setting.MessegeBox.SetMessege("ابتدا باید ثبت نام کنید.");
                else
                    Setting.MessegeBox.SetMessege("ارتباط با سرور برقرار نشد. اینترنت خود را چک کنید.");

            }
            else
            {
                if (PlayerPrefs.GetInt(alreadyRegistered) == 0)
                    PlayerPrefs.SetInt(alreadyRegistered,1) ;

                SceneManager.LoadScene(Setting.mainScene);

            }
        }, 10000);

    }
    // Function for save age and gender
    public void saveAgegen()
    {

        try
        {
            BacktoryObject genderage = new BacktoryObject("GenderAge");
            genderage["gender"] = sexDropDown.value;
            genderage["age"] = ageDropDown.value;
            genderage["userID"] = BacktoryUser.CurrentUser.UserId;


            genderage.SaveInBackground(response =>
            {
                if (response.Successful)
                {
                    PlayerPrefs.SetInt(alreadyRegistered, 1);
                    // successful save. good place for Debug.Log function.

                }
                else
                {
                    // see response.Message to know the cause of error
                }
            });
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }

    }
    // functions for popup windows

    // Email address validation function
    bool IsValidEmail(string email)
    {
        try
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            else
                return false;
        }
        catch (FormatException)
        {
            return false;
        }
    }
    // Forgetting password function
    public void onForgotpassClick()
    {
        string username = usernameInputlog.text;

        // Requesting forget password to backtory
        BacktoryUser.ForgotPasswordInBackground(username, response =>
        {
            if (response.Successful)
            {
                Setting.MessegeBox.SetMessege("کلمه عبور جدید به ایمیلت ارسال شد.");
                // Showforgotpass();
                // Debug.Log("Go to your mail inbox and verify your request.");
            }
            else
            {
                Debug.Log("failed; " + response.Message);
            }
        });
    }
}