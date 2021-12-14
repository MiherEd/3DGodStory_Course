using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{


    #region 靜態變數
    static public string UserAccount;
    static public string UserPassword;
    static public string UserName;
    static public int Score_;
    static public string LoginPlayerAcc;
    static public string LoginPlayerPassword;
    static public int Level = 1;
    #endregion



    [Header("怪物產生的位置")]
    public GameObject CreateNPCPos;
    [Header("多久產生一隻怪物")]
    public float CreateTime;
    [Header("怪物總數量")]
    public float MaxNPCNum;
    [Header("怪物目前數量")]
    public float NPCNum;
    [Header("怪物")]
    public GameObject NPC;
    [Header("總得分")]
    public int TotalScore;
    [Header("NPC死亡家分")]
    public int NPCAddScore;
    [Header("分數文字")]
    public Text ScoreText;

    [Header("剩餘多少怪物能量")]
    public Image NPCNumBar;
    float DieNum;

    [Header("玩家的總血量")]
    public float TotalPlayerHP;
    //程式中玩家的血量
    float ScriptTotalPlayerHP;
    [Header("NPC打玩家的扣血量")]
    public float HurtPlayerHP;
    [Header("玩家血條")]
    public Image PlayerHPBar;

    [Header("多少時間後可以開啟大魔法")]
    public float CreateMagicTime;
    float ScriptMagicTime;
    [Header("大絕招的按鈕")]
    public Button MagicButton;
    [Header("施放絕招條")]
    public Image MagicBar;

    [Header("Boss物件")]
    public GameObject BossObject;

    [Header("PauseUI")]
    public GameObject PauseUI;

    [Header("玩家腳本")]
    public Player PlayerScript;

    [Header("GameOverUI")]
    public GameObject GameOVerUI;
    [Header("Win")]
    public Sprite WinSprite;
    [Header("Lose")]
    public Sprite LoseSprite;
    [Header("顯示勝利和失敗的圖")]
    public Image FinalImage;

    [Header("數字0-9圖片")]
    public Sprite[] NumberSprites;
    [Header("十進位Image")]
    public Image LevelImage;
    [Header("個進位Image")]
    public Image LevelImage2;

    [Header("遊戲結束畫面_十進位Image")]
    public Image GameOver_LevelImage1;
    [Header("遊戲結束畫面_個進位Image")]
    public Image GameOver_LevelImage2;


    [Header("獎勵值")]
    public int AwardScore;
    int StartNum = 0;
    int EndNum;
    int Jump = 10;
    int result;
    [Header("遊戲結束畫面_獎勵數值文字")]
    public Text AwardScoreText;

    //總分跳動數值起使值
    int TotalScoreStartNum = 0;
    //總分跳動數值結束值
    int TotalScoreEndNum = 0;
    [Header("遊戲結束畫面_總分數文字")]
    public Text TotalScoreText;
    //程式中計算值
    int TotalScoreResult;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateNPC", CreateTime, CreateTime);
        ScriptTotalPlayerHP = TotalPlayerHP;
        MagicButton.interactable = false;
        MagicBar.fillAmount = 0;
        UnPauseGame();
        //數字放入圖片第一種寫法
        First_Level();
        //數字放入圖片第二種寫法
        Second_Level();
    }

    // Update is called once per frame
    void Update()
    {
        ScriptMagicTime += Time.deltaTime;
        MagicBar.fillAmount = ScriptMagicTime / CreateMagicTime;
        if (MagicBar.fillAmount == 1)
            MagicButton.interactable = true;
        else
            MagicButton.interactable = false;    

    }

    void CreateNPC()
    {
        if (NPCNum < MaxNPCNum)
        {
            //Collider邊界最大值
            Vector3 MaxBounds = CreateNPCPos.GetComponent<Collider>().bounds.max;
            //Collider邊界最小值
            Vector3 MinBounds = CreateNPCPos.GetComponent<Collider>().bounds.min;
            //計算NPC要產生的位置範圍
            Vector3 Random_Pos = new Vector3(Random.Range(MinBounds.x, MaxBounds.x), 0, MaxBounds.z);
            Instantiate(NPC, CreateNPCPos.transform.position, CreateNPCPos.transform.rotation);
            NPCNum++;
        }
    }
    public void MonsterDie()
    {
        DieNum++;        
        NPCNumBar.fillAmount = 1f- (DieNum / MaxNPCNum);
        if (NPCNumBar.fillAmount == 0)
            CreatBoss();
    }
    public void Score()
    {
        TotalScore += NPCAddScore;
        ScoreText.text = TotalScore + "";
    }
    public void HurtPlayer()
    {
        ScriptTotalPlayerHP -= HurtPlayerHP;
        PlayerHPBar.fillAmount = ScriptTotalPlayerHP / TotalPlayerHP;
        if(PlayerHPBar.fillAmount == 0)
        {
            GameOver();
        }
    }
    public void Reset()
    {
        ScriptMagicTime = 0;
    }
    void CreatBoss()
    {
        if(GameObject.FindGameObjectsWithTag("Boss").Length <= 0)
        {
            Instantiate(BossObject, CreateNPCPos.transform.position, CreateNPCPos.transform.rotation);
        }
    }
    public void BossHurtPlayer()
    {
        ScriptTotalPlayerHP -= HurtPlayerHP * 2;
        PlayerHPBar.fillAmount = ScriptTotalPlayerHP / TotalPlayerHP;
        if (PlayerHPBar.fillAmount == 0)
        {
            GameOver();
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        PlayerScript.enabled = false;
        PauseUI.SetActive(true);
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1;
        PlayerScript.enabled = true;
        PauseUI.SetActive(false);
    }
    public void BackMenu()
    {
        Time.timeScale = 1;
        Application.LoadLevel("Menu");
    }
    public void GameOver()
    {
        //Time.timeScale = 0;
        GameOVerUI.SetActive(true);
        if (PlayerHPBar.fillAmount == 0)
        {
            FinalImage.sprite = LoseSprite;
            EndNum = 0;
            TotalScoreEndNum = TotalScore;
        }
        else
        {
            FinalImage.sprite = WinSprite;
            EndNum = AwardScore;
            TotalScoreEndNum = TotalScore+AwardScore;
        }
        StartCoroutine(JumpNumber());
        StartCoroutine(JumpTotalNumber());
    }

    //數字放如圖片第一種寫法
    void First_Level()
    {
        //十進位
        LevelImage.sprite = NumberSprites[Level / 10];
        //個進位
        LevelImage2.sprite = NumberSprites[Level % 10];
    }
    void Second_Level()
    {
        string LevelString = Level.ToString();
        if (LevelString.Length >= 2)
        {
            GameOver_LevelImage1.sprite = NumberSprites[int.Parse(LevelString.Substring(0, 1))];
            GameOver_LevelImage2.sprite = NumberSprites[int.Parse(LevelString.Substring(1, 1))];
        }
        else
        {
            GameOver_LevelImage1.sprite = NumberSprites[0];
            GameOver_LevelImage2.sprite = NumberSprites[Level];
        }
    }

    IEnumerator JumpNumber()
    {
        int delta = (EndNum - StartNum) / Jump;
        result = 0;
        for(int i =0; i<Jump; i++)
        {
            result += delta;
            AwardScoreText.text = result.ToString();
            yield return new WaitForSeconds(0.1f);
        }
        result = EndNum;
        AwardScoreText.text = result.ToString();
        StopCoroutine(JumpNumber());
        Time.timeScale = 0;
    }

    IEnumerator JumpTotalNumber()
    {
        int delta = (TotalScoreEndNum - TotalScoreStartNum) / Jump;
        TotalScoreResult = 0;
        for (int i = 0; i < Jump; i++)
        {
            TotalScoreResult += delta;
            TotalScoreText.text = TotalScoreResult.ToString();
            yield return new WaitForSeconds(0.1f);
        }
        TotalScoreResult = TotalScoreEndNum;
        TotalScoreText.text = TotalScoreResult.ToString();
        StopCoroutine(JumpTotalNumber());
        Time.timeScale = 0;
    }

    public void Ragame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
