using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//引用Unity UI程式庫
using UnityEngine.UI;
//引用Proyecto26程式庫才能將上傳與讀取Firebase資料庫
using Proyecto26;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.IO;

public class PassWord : MonoBehaviour
{
    [Header("錯誤信息文字")]
    public Text ErrorMessage;
    [Header("帳號輸入框")]
    public InputField AccountInputField;
    [Header("信箱輸入框")]
    public InputField EmailInputField;
    User user = new User();
    [Header("登入頁面")]
    public GameObject LoginPage;

    [Header("原始資料")]
    public string OriginalData;
    public string[] OriginalDatas;
    public List<string> Datas;

    //查詢
    public void Inquire()
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
        //如果帳號輸入框已經在Firebase有相同的名稱，再接著檢驗密碼是否正確
        if (OriginalData != "null")
        {
            if (Datas[3] == AccountInputField.text)
            {
                string acc = "miher.ed";
                string pass = "Edstoryee0505";
                MailMessage Mail = new MailMessage();

                Mail.From = new MailAddress(acc);
                Mail.To.Add(acc);
                Mail.Subject = "密碼找回";
                Mail.Body = Datas[5]+ " 玩家您好，這是您的密碼   ： " + Datas[7];

                //傳送副檔案
                /* if (!string.IsNullOrEmpty(檔案位置))
                 {
                     Mail.Attachments.Add(new Attachment(檔案位置));
                 }*/

                SmtpClient smtpServer = new SmtpClient();
                smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpServer.Host = "smtp.gmail.com";
                smtpServer.Port = 587;
                smtpServer.Credentials = new System.Net.NetworkCredential(acc, pass) as ICredentialsByHost;
                smtpServer.EnableSsl = true;
                ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    { return true; };
                smtpServer.Send(Mail);
                ErrorMessage.text = "信件發送成功";

            }
        }
        //帳號錯誤
        else
        {
            ErrorMessage.text = "尚未有此帳號";
        }
    }
    public void BackLogin()
    {
        LoginPage.SetActive(true);
        gameObject.SetActive(false);
    }
}
