using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;


public class Login : MonoBehaviour
{
    [Header("�b����J��")]
    public InputField AccountInputField;
    [Header("�K�X��J��")]
    public InputField PasswordInputField;
    User user = new User();
    [Header("���~�T����r")]
    public Text ErrorMessage;
    [Header("�C������")]
    public GameObject GameMenuPage;
    [Header("���U����")]
    public GameObject RegisterPage;
    [Header("�ק�K�X����")]
    public GameObject ChangePasswordPage;

    [Header("��l���")]
    public string OriginalData;
    public string[] OriginalDatas;
    public List<string> Datas;


    //Ū��Firebase���
    public void LoginLoadData()
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
        //�p�G�b����J�ؤw�g�bFirebase���ۦP���W�١AErrorMessage�N��ܵ��U�L���H��
        if (OriginalData != "null")
        {
            if (Datas[3] == AccountInputField.text)
            {
                //�K�X���T
                if (Datas[5] == PasswordInputField.text)
                {
                    ErrorMessage.text = "�n�J���\!";
                    StartCoroutine(ToManu(2f));
                }
                else
                {
                    ErrorMessage.text = "�K�X���~!" + PasswordInputField.text;
                }
            }
        }
        //�b�����~
        else
        {
            ErrorMessage.text = "�b����J���~!\n" + user.UserAccount + "/" + AccountInputField.text;
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
