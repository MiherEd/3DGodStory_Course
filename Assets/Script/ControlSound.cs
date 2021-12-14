using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class ControlSound : MonoBehaviour
{
    [Header("控制聲音")]
    public bool Control;
    [Header("讀取聲音關閉的圖片")]
    public Sprite SoundClose;
    [Header("讀取聲音開啟的圖片")]
    public Sprite SoundOpen;
    [Header("聲音按鈕")]
    public Image SoundButton;

    void Start()
    {
        //讀取Resources資料夾內格式為Sprite 且檔名為sound_close圖片
        SoundClose = Resources.Load<Sprite>("sound_close");
        SoundOpen = Resources.Load<Sprite>("sound_open");
        //從StreamingAssets資料夾讀取圖片寫法
        /*string folderPath = Application.streamingAssetsPath + "/sound_close.png";

        //將StreamingAssets裡面的圖片轉換成Byte形式
        byte[] pngBytes = File.ReadAllBytes(folderPath);
        //宣告Texture2D變數
        Texture2D tex = new Texture2D(2, 2);
        //將Byte形式的圖片轉換成Texture2D
        tex.LoadImage(pngBytes);
        //Sprite讀取Texture2D圖片
        SoundClose = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        //Image讀取Sprite圖片
        tex.LoadImage(pngBytes);
        */
    }

    void Update()
    {
        
    }
    public void Control_Sound()
    {
        Control = !Control;
        AudioListener.pause = Control;
        if (Control)
            SoundButton.sprite = SoundOpen;
        else
            SoundButton.sprite = SoundClose;
    }
}
