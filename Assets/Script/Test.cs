using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�ޥ�Unity UI�{���w
using UnityEngine.UI;
//�ޥ�Proyect26�{���w�~��N�W�ǻPŪ��Firebase��Ʈw
using Proyecto26;

public class Test : MonoBehaviour
{
    //------------------------------------�t�@�ا��Firebase��Ƥ覡
    [Header("�b����J��")]
    public InputField AccountInputField;
    [Header("��l���")]
    public string OriginalData;
    public string[] OriginalDatas;
    public List<string> Datas;
    public void GetFirebaseData()
    {
        RestClient.Get("https://secondgame-54e30-default-rtdb.firebaseio.com/" + AccountInputField.text + ".json").Then(
               response =>
               {
                   OriginalData = response.Text;
               }
           );
        StartCoroutine(WaitAndPrint1(2f));

    }
    IEnumerator WaitAndPrint1(float waitTime)
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

    }
}
