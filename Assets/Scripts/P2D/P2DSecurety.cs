using UnityEngine;
using System.Collections;

public class P2DSecurety  {

    static string md5HashLocal = "vFe5peauy[1]c-Jpdn";


    static public bool DecodeIsEqual(string hashValue,string value)
    {

        if (hashValue.Equals(Md5Sum(value)))
        {
            return true;
        }
        else
        {
            return false;
        }

    }


    public static string Md5Sum(string s)
    {

        s += md5HashLocal;
        System.Security.Cryptography.MD5 hashAlgoritm = System.Security.Cryptography.MD5.Create();
        byte[] data = hashAlgoritm.ComputeHash(System.Text.Encoding.Default.GetBytes(s));

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
            sb.Append(data[i].ToString("x2"));
        }
        return sb.ToString();
    }

    public static string Md5SumPHP(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }

    const string hashKey = "Hash";

    public static void SecureLocalSave(string key, string value)
    {
        PlayerPrefs.SetString(key, value);

        PlayerPrefs.SetString(key + hashKey, Md5Sum(value));

    }

    public static void SecureLocalSave(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);

        PlayerPrefs.SetString(key + hashKey, Md5Sum(value.ToString()));

    }

    public static void SecureLocalSave(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);

        PlayerPrefs.SetString(key + hashKey, Md5Sum(value.ToString()));

    }

    public static void SecureLocalSave(string key, bool value)
    {
        int intValue = value ? 1 : 0;
        PlayerPrefs.SetInt(key, intValue);

        PlayerPrefs.SetString(key + hashKey, Md5Sum(intValue.ToString()));

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns>if load fail or securety problem return false</returns>
    public static bool SecureLocalLoad(string key, out string value)
    {
        value = PlayerPrefs.GetString(key);
        string savedHash = PlayerPrefs.GetString(key + hashKey);

        string realHash = Md5Sum(value);

        if (realHash == savedHash)
            return true;
        else
            return false;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns>if load fail or securety problem return false and value 0</returns>
    public static bool SecureLocalLoad(string key, out float value)
    {
        value = PlayerPrefs.GetFloat(key);
        string savedHash = PlayerPrefs.GetString(key + hashKey);

        string realHash = Md5Sum(value.ToString());

        if (realHash == savedHash)
            return true;
        else
        {
            value = 0;
            return false;
        }
    }


    /// <summary>
    /// if load fail or securety problem return false
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns>if load fail or securety problem return false and value 0</returns>
    public static bool SecureLocalLoad(string key, out int value)
    {
        value = PlayerPrefs.GetInt(key);
        string savedHash = PlayerPrefs.GetString(key + hashKey);

        string realHash = Md5Sum(value.ToString());

        if (realHash == savedHash)
            return true;
        else
        {
            value = 0;
            return false;
        }
    }


    /// <summary>
    /// if load fail or securety problem return false
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns>if load fail or securety problem return false and value 0</returns>
    public static bool SecureLocalLoad(string key, out bool value)
    {
        int temp = PlayerPrefs.GetInt(key);
        if (temp != 0)
            value = true;
        else
            value = false;

        string savedHash = PlayerPrefs.GetString(key + hashKey);

        string realHash = Md5Sum(temp.ToString());

        if (realHash == savedHash)
            return true;
        else
        {
            value = false;
            return false;
        }
    }


}
