using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�ޥ�Unity UI�{���w
using UnityEngine.UI;
//�ޥ�Proyecto26�{���w�~��N�W�ǻPŪ��Firebase��Ʈw
using Proyecto26;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.IO;

public class PassWord : MonoBehaviour
{
    [Header("���~�H����r")]
    public Text ErrorMessage;
    [Header("�b����J��")]
    public InputField AccountInputField;
    [Header("�H�c��J��")]
    public InputField EmailInputField;
    User user = new User();
    [Header("�n�J����")]
    public GameObject LoginPage;

    [Header("��l���")]
    public string OriginalData;
    public string[] OriginalDatas;
    public List<string> Datas;

    //�d��
    public void Inquire()
    {
        //�M��ErrorMessage�̭������e
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
        //�p�G�b����J�ؤw�g�bFirebase���ۦP���W�١A�A��������K�X�O�_���T
        if (OriginalData != "null")
        {
            if (Datas[3] == AccountInputField.text)
            {
                string acc = "miher.ed";
                string pass = "Edstoryee0505";
                MailMessage Mail = new MailMessage();

                Mail.From = new MailAddress(acc);
                Mail.To.Add(acc);
                Mail.Subject = "�K�X��^";
                Mail.Body = Datas[5]+ " ���a�z�n�A�o�O�z���K�X   �G " + Datas[7];

                //�ǰe���ɮ�
                /* if (!string.IsNullOrEmpty(�ɮצ�m))
                 {
                     Mail.Attachments.Add(new Attachment(�ɮצ�m));
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
                ErrorMessage.text = "�H��o�e���\";

            }
        }
        //�b�����~
        else
        {
            ErrorMessage.text = "�|�������b��";
        }
    }
    public void BackLogin()
    {
        LoginPage.SetActive(true);
        gameObject.SetActive(false);
    }
}
