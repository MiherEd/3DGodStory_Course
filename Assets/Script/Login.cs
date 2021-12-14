using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;


public class Login : MonoBehaviour
{
    [Header("帳號輸入框")]
    public InputField AccountInputField;
    [Header("密碼輸入框")]
    public InputField PasswordInputField;
    User user = new User();
    [Header("錯誤訊息文字")]
    public Text ErrorMessage;
    [Header("遊戲頁面")]
    public GameObject GameMenuPage;
    [Header("註冊頁面")]
    public GameObject RegisterPage;
    [Header("修改密碼頁面")]
    public GameObject ChangePasswordPage;

    [Header("原始資料")]
    public string OriginalData;
    public string[] OriginalDatas;
    public List<string> Datas;


    //讀取Firebase資料
    public void LoginLoadData()
    {
        //清除ErrorMessage裡面的內容
        ErrorMessage.text = "";
        RestClient.Get("https://classwork-d5d4e-default-rtdb.europe-west1.firebasedatabase.app/" + AccountInputField.text + ".json").Then(
           response =>
           {
               OriginalData = response.Text;
           }
        );
        StartCoroutine(WaitAndPrint(2f));
    }
    IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Datas.Clear();
        OriginalDatas = OriginalData.Split('{', '}', ',', ':', '"');
        for (int i = 0; i < OriginalDatas.Length; i++)
        {
            if (OriginalDatas[i] != "")
            {
                Datas.Add(OriginalDatas[i]);
            }
        }
        //如果帳號輸入框已經在Firebase有相同的名稱，ErrorMessage就顯示註冊過的信息
        if (OriginalData != "null")
        {
            if (Datas[3] == AccountInputField.text)
            {
                //密碼正確
                if (Datas[5] == PasswordInputField.text)
                {
                    ErrorMessage.text = "登入成功!";
                    StartCoroutine(ToManu(2f));
                }
                else
                {
                    ErrorMessage.text = "密碼錯誤!" + PasswordInputField.text;
                }
            }
        }
        //帳號錯誤
        else
        {
            ErrorMessage.text = "帳號輸入錯誤!\n" + user.UserAccount + "/" + AccountInputField.text;
        }
    }
    IEnumerator ToManu(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        staticvar.LoginPlayerAcc = AccountInputField.text;
        staticvar.LoginPlayerPassword = PasswordInputField.text;
        GameMenuPage.SetActive(true);
        gameObject.SetActive(false);
    }
    public void ToRegister()
    {
        RegisterPage.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ToChangePassword()
    {
        ChangePasswordPage.SetActive(true);
        gameObject.SetActive(false);
    }
}
