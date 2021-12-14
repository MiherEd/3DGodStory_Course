using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�ޥ�UI�{���w
using UnityEngine.UI;
//�ޥ�Proyecto26�{���w�~��W�ǻPŪ��Firebase��Ʈw
using Proyecto26;

public class Register : MonoBehaviour
{
    [Header("�b����J��")]
    public InputField AccountInputField;
    [Header("�K�X��J��")]
    public InputField PasswordInputField;
    [Header("�ϥΪ̦W�ٿ�J��")]
    public InputField UserNameInputField;
    [Header("���~�H��")]
    public Text ErrorMessage;

    [Header("�n�J����")]
    public GameObject LoginPage;
    [Header("���U����")]
    public GameObject RegisterPage;

    [Header("��l���")]
    public string OriginalData;
    public string[] OriginalDatas;
    public List<string> Datas;


    //���U���U�����s�N��ƶǰe��Firebase
    public void Onsubmit()
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
                ErrorMessage.text = "�w�g���ۦP�b���A�Э��s���U�C";
        }
        else
        {
            //�b���Ȧs������ܼ�
            staticvar.UserAccount = AccountInputField.text;
            //�b���Ȧs������ܼ�
            staticvar.UserPassword = PasswordInputField.text;
            //�ϥΪ̦W�ټȦs������ܼ�
            staticvar.UserName = UserNameInputField.text;
            //�W�Ǹ�ƨ�Firebase;
            PostToDatabase();
            ErrorMessage.text = "���U���\";
        }
    }
    void PostToDatabase()
    {
        User user = new User();
        //RestClient.Put �W�Ǹ�ƨ�Firebase("Firebase���}"+�b��+".json",user�榡���);
        RestClient.Put("https://classwork-d5d4e-default-rtdb.europe-west1.firebasedatabase.app/" + AccountInputField.text + ".json", user);
    }
    public void BackLogin()
    {
        LoginPage.SetActive(true);
        RegisterPage.SetActive(false);
    }
}
