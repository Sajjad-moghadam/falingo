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

    // Popup Window
    public MessageBoxEx myMessageBox;
    public MessageBoxEx myMessageBoxlogin;

    public void Start()
    {
#if UNITY_EDITOR
        //PlayerPrefs.DeleteAll(); //danger*****
#endif
        // load local
        if (PlayerPrefs.GetInt(alreadyRegistered) == 1)
        {
            //string savedusername = PlayerPrefs.GetString(usernameKey);
            //string savedpass = PlayerPrefs.GetString(passKey);
            LoginProcess(true);
        }
    }

    public void onRegisterClick()
    {
        // First create a user and fill his/her data
        BacktoryUser newUser = new BacktoryUser
        {
            FirstName = usernameInputreg.text,
            Username = SystemInfo.deviceUniqueIdentifier,
            Password = SystemInfo.deviceUniqueIdentifier

        };

        //if(Regex.IsMatch(usernameInputreg.text, "^[a-zA-Z0-9]*$") && (usernameInputreg.text != "") && (emailInputreg.text != "") && (passwordInputreg.text != "") && (IsValidEmail(emailInputreg.text))){
        if( (usernameInputreg.text != "") ){

            // Registring user to backtory (in background)
            newUser.RegisterInBackground(response =>
            {
                // Checking result of operation
                if (response.Successful)
                {
                    // save local
                    PlayerPrefs.SetString(usernameKey, newUser.FirstName);
                    //PlayerPrefs.SetString(emailKey, newUser.Email);
                    //PlayerPrefs.SetString(passKey, newUser.Password);

                    // register complated and we should login now
                    LoginProcess(true);


                }
                else if (response.Code == (int)BacktoryHttpStatusCode.Conflict)
                {
                    myMessageBox.SetMessage("نام کاربری وارد شده موجود می باشد.");
                    // Showbaduser();
                    // Username is invalid
                    Debug.Log("Bad username; a user with this username already exists.");
                }
                else
                {
                    myMessageBox.SetMessage("مشکلی در شبکه بوجود آمده، لطفا دوباره تلاش کنید.");
                    // Shownetdownregister();
                    // General failure
                    Debug.Log("Registration failed; for network or some other reasons.");
                }
            });
        }
        //else if(!(Regex.IsMatch(usernameInputreg.text, "^[a-zA-Z0-9]*$"))){
            
        //    myMessageBox.SetMessage("لطفا نام کاربری خود را انگلیسی وارد کنید.");
        //    // Showenglishusername();
        //    // Debug.Log("Oops");
        //}
        else if((usernameInputreg.text == "")){
            
            myMessageBox.SetMessage("لطفا نام کاربری خود را وارد کنید.");
            // Showemptyusername();
            // Debug.Log("Oops");
        }
        //else if((emailInputreg.text == "")){

        //    myMessageBox.SetMessage("لطفا ایمیل خود را وراد کنید.");
        //    // Showemptyemail();
        //    // Debug.Log("Oops");
        //}
        //else if(!(IsValidEmail(emailInputreg.text))){

        //    myMessageBox.SetMessage("ایمیل وارد شده صحیح نمی باشد.");
        //    // Showfalsemail();
        //    // Debug.Log("Oops");
        //}
        //else if((passwordInputreg.text == "")){

        //    myMessageBox.SetMessage("لطفا کلمه عبور خود را وارد کنید.");
        //    // Showemptypassword();
        //    // Debug.Log("Oops");
        //}

    }

    public void LogInClick()
    {

        if (usernameInputlog.text != ""){
            
            LoginProcess(false);

        }else if ((usernameInputlog.text == ""))
        {
            myMessageBoxlogin.SetMessage("لطفا نام کاربری خود را وارد کنید.");
            //Showemptyusername();
            //Debug.Log("Oops");
        }

    }

    public void LoginProcess(bool newUser)
    {
        
        BacktoryUser.LoginInBackground(SystemInfo.deviceUniqueIdentifier, SystemInfo.deviceUniqueIdentifier, loginResponse =>
        {

            // Login operation done (fail or success), handling it:
            if (loginResponse.Successful)
            {
                Debug.Log("Login Successful.");
                SceneManager.LoadScene("MainMenu");

                // We have UserId and if it is the first time that he logs in, we should send age and gender to Backtory.
                if (PlayerPrefs.GetInt(alreadyRegistered) != 1)
                {
                    if (newUser)
                    {
                        saveAgegen();
                    }
                    // If the user is a member of service and because of exchanging his phone or clearing his playerprefs' data,
                    // we can read his age/gen data locally. 
                    else
                    {
                       //TODO: LoadAgeGen()
                    }
                }

            }
            else if (loginResponse.Code == (int)BacktoryHttpStatusCode.Unauthorized)
            {
                myMessageBoxlogin.SetMessage("نام کاربری یا کلمه عبور وارد شده صحیح نمی باشد.");
                // Showwrongmailusername();
                // Username 'mohx' with password '123456' is wrong
                Debug.Log("Either username or password is wrong.");
            }
            else
            {
                myMessageBoxlogin.SetMessage("مشکلی در شبکه بوجود آمده، لطفا دوباره تلاش کنید.");
                // Shownetdownlog();
                // Operation generally failed, maybe internet connection issue
                Debug.Log("Login failed for other reasons like network issues.");
            }
        });

    }
    // Function for save age and gender
    public void saveAgegen()
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
        BacktoryUser.ForgotPasswordInBackground(username, response => {
            if (response.Successful)
            {
                myMessageBoxlogin.SetMessage("کلمه عبور جدید به ایمیلت ارسال شد.");
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