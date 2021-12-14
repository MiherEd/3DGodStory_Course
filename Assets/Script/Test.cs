using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//引用Unity UI程式庫
using UnityEngine.UI;
//引用Proyect26程式庫才能將上傳與讀取Firebase資料庫
using Proyecto26;

public class Test : MonoBehaviour
{
    //------------------------------------另一種抓取Firebase資料方式
    [Header("帳號輸入框")]
    public InputField AccountInputField;
    [Header("原始資料")]
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
