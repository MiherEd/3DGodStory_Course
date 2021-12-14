using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class ControlSound : MonoBehaviour
{
    [Header("�����n��")]
    public bool Control;
    [Header("Ū���n���������Ϥ�")]
    public Sprite SoundClose;
    [Header("Ū���n���}�Ҫ��Ϥ�")]
    public Sprite SoundOpen;
    [Header("�n�����s")]
    public Image SoundButton;

    void Start()
    {
        //Ū��Resources��Ƨ����榡��Sprite �B�ɦW��sound_close�Ϥ�
        SoundClose = Resources.Load<Sprite>("sound_close");
        SoundOpen = Resources.Load<Sprite>("sound_open");
        //�qStreamingAssets��Ƨ�Ū���Ϥ��g�k
        /*string folderPath = Application.streamingAssetsPath + "/sound_close.png";

        //�NStreamingAssets�̭����Ϥ��ഫ��Byte�Φ�
        byte[] pngBytes = File.ReadAllBytes(folderPath);
        //�ŧiTexture2D�ܼ�
        Texture2D tex = new Texture2D(2, 2);
        //�NByte�Φ����Ϥ��ഫ��Texture2D
        tex.LoadImage(pngBytes);
        //SpriteŪ��Texture2D�Ϥ�
        SoundClose = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        //ImageŪ��Sprite�Ϥ�
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
