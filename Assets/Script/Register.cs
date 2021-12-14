using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//引用UI程式庫
using UnityEngine.UI;
//引用Proyecto26程式庫才能上傳與讀取Firebase資料庫
using Proyecto26;

public class Register : MonoBehaviour
{
    [Header("帳號輸入框")]
    public InputField AccountInputField;
    [Header("密碼輸入框")]
    public InputField PasswordInputField;
    [Header("使用者名稱輸入框")]
    public InputField UserNameInputField;
    [Header("錯誤信息")]
    public Text ErrorMessage;

    [Header("登入頁面")]
    public GameObject LoginPage;
    [Header("註冊頁面")]
    public GameObject RegisterPage;

    [Header("原始資料")]
    public string OriginalData;
    public string[] OriginalDatas;
    public List<string> Datas;


    //按下註冊的按鈕將資料傳送到Firebase
    public void Onsubmit()
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
                ErrorMessage.text = "已經有相同帳號，請重新註冊。";
        }
        else
        {
            //帳號暫存於全域變數
            staticvar.UserAccount = AccountInputField.text;
            //帳號暫存於全域變數
            staticvar.UserPassword = PasswordInputField.text;
            //使用者名稱暫存於全域變數
            staticvar.UserName = UserNameInputField.text;
            //上傳資料到Firebase;
            PostToDatabase();
            ErrorMessage.text = "註冊成功";
        }
    }
    void PostToDatabase()
    {
        User user = new User();
        //RestClient.Put 上傳資料到Firebase("Firebase網址"+帳號+".json",user格式資料);
        RestClient.Put("https://classwork-d5d4e-default-rtdb.europe-west1.firebasedatabase.app/" + AccountInputField.text + ".json", user);
    }
    public void BackLogin()
    {
        LoginPage.SetActive(true);
        RegisterPage.SetActive(false);
    }
}
