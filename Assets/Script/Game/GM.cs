using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{


    #region �R�A�ܼ�
    static public string UserAccount;
    static public string UserPassword;
    static public string UserName;
    static public int Score_;
    static public string LoginPlayerAcc;
    static public string LoginPlayerPassword;
    static public int Level = 1;
    #endregion



    [Header("�Ǫ����ͪ���m")]
    public GameObject CreateNPCPos;
    [Header("�h�[���ͤ@���Ǫ�")]
    public float CreateTime;
    [Header("�Ǫ��`�ƶq")]
    public float MaxNPCNum;
    [Header("�Ǫ��ثe�ƶq")]
    public float NPCNum;
    [Header("�Ǫ�")]
    public GameObject NPC;
    [Header("�`�o��")]
    public int TotalScore;
    [Header("NPC���`�a��")]
    public int NPCAddScore;
    [Header("���Ƥ�r")]
    public Text ScoreText;

    [Header("�Ѿl�h�֩Ǫ���q")]
    public Image NPCNumBar;
    float DieNum;

    [Header("���a���`��q")]
    public float TotalPlayerHP;
    //�{�������a����q
    float ScriptTotalPlayerHP;
    [Header("NPC�����a������q")]
    public float HurtPlayerHP;
    [Header("���a���")]
    public Image PlayerHPBar;

    [Header("�h�֮ɶ���i�H�}�Ҥj�]�k")]
    public float CreateMagicTime;
    float ScriptMagicTime;
    [Header("�j���۪����s")]
    public Button MagicButton;
    [Header("�I�񵴩۱�")]
    public Image MagicBar;

    [Header("Boss����")]
    public GameObject BossObject;

    [Header("PauseUI")]
    public GameObject PauseUI;

    [Header("���a�}��")]
    public Player PlayerScript;

    [Header("GameOverUI")]
    public GameObject GameOVerUI;
    [Header("Win")]
    public Sprite WinSprite;
    [Header("Lose")]
    public Sprite LoseSprite;
    [Header("��ܳӧQ�M���Ѫ���")]
    public Image FinalImage;

    [Header("�Ʀr0-9�Ϥ�")]
    public Sprite[] NumberSprites;
    [Header("�Q�i��Image")]
    public Image LevelImage;
    [Header("�Ӷi��Image")]
    public Image LevelImage2;

    [Header("�C�������e��_�Q�i��Image")]
    public Image GameOver_LevelImage1;
    [Header("�C�������e��_�Ӷi��Image")]
    public Image GameOver_LevelImage2;


    [Header("���y��")]
    public int AwardScore;
    int StartNum = 0;
    int EndNum;
    int Jump = 10;
    int result;
    [Header("�C�������e��_���y�ƭȤ�r")]
    public Text AwardScoreText;

    //�`�����ʼƭȰ_�ϭ�
    int TotalScoreStartNum = 0;
    //�`�����ʼƭȵ�����
    int TotalScoreEndNum = 0;
    [Header("�C�������e��_�`���Ƥ�r")]
    public Text TotalScoreText;
    //�{�����p���
    int TotalScoreResult;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateNPC", CreateTime, CreateTime);
        ScriptTotalPlayerHP = TotalPlayerHP;
        MagicButton.interactable = false;
        MagicBar.fillAmount = 0;
        UnPauseGame();
        //�Ʀr��J�Ϥ��Ĥ@�ؼg�k
        First_Level();
        //�Ʀr��J�Ϥ��ĤG�ؼg�k
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
            //Collider��ɳ̤j��
            Vector3 MaxBounds = CreateNPCPos.GetComponent<Collider>().bounds.max;
            //Collider��ɳ̤p��
            Vector3 MinBounds = CreateNPCPos.GetComponent<Collider>().bounds.min;
            //�p��NPC�n���ͪ���m�d��
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

    //�Ʀr��p�Ϥ��Ĥ@�ؼg�k
    void First_Level()
    {
        //�Q�i��
        LevelImage.sprite = NumberSprites[Level / 10];
        //�Ӷi��
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
