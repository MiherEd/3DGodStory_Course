using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("滑鼠點到的座標位置")]
    public Vector3 Target_Pos;
    [Header("玩家注視方向")]
    public Vector3 Look_Pos;
    [Header("收集射線打到的物件")]
    public RaycastHit[] hits;
    RaycastHit hit;
    [Header("玩家動作")]
    public Animator PlayerAimator;
    [Header("玩家普攻物件")]
    public GameObject FireObj;
    [Header("普攻物件生成的位置")]
    public GameObject CreateFireObj;

    public bool CanMagic;
    [Header("大絕招物件")]
    public GameObject MagicObject;
    //儲存大絕招Prefab物件
    GameObject MagicObject_Prefab;
    [Header("施放絕招條")]
    public Image MagicBar;
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* 射線分為兩個方式 
         * 一個是 Raycast:
         * 發射出來 碰到物件 就會停止
         * 另一種是 RaycastAll:
         **/
        //Input.GetMouseButton
        //Input.GetMouseButtonDown 點一次放一次
        //Input.GetMouseButtonUp 放開才觸發
        //0:滑鼠左鍵 1:滑鼠右鍵 2:中間滾輪
        if (Input.GetMouseButton(0))
        {
            //抓到滑鼠點擊的3維位置和主攝影機的3維座標位置連成一線->射線
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;
            //射線打到的所有Collider的物件暫存在hits陣列中
            hits = Physics.RaycastAll(ray, 100);
            /*
            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.red);
                if (hit.collider.name == "mazu_floor")
                {
                    //抓取滑鼠點擊到的X、Z值，Y值不用抓取
                    Target_Pos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                    //使用內插法方式讓玩家從A點轉到B點
                    Look_Pos = Vector3.Lerp(Look_Pos, Target_Pos, Time.deltaTime * 10);
                    //LookAt面相(玩家注視位置點)
                    transform.LookAt(Look_Pos);
                }
            }*/
            //使用for迴圈判斷hits陣列中有沒有地板這個物件
            for(int i = 0; i< hits.Length; i++)
            {
                hit = hits[i];
                if (hit.collider.name == "mazu_floor")
                {
                    //CanMagic = true 可以使用大絕招
                    if (CanMagic)
                    {
                        if (GameObject.FindGameObjectsWithTag("Magic").Length <= 0)
                            MagicObject_Prefab = Instantiate(MagicObject, transform.position, transform.rotation) as GameObject;

                        //大絕招的物件位置 = 射線打到地板的位置
                        MagicObject_Prefab.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                        //旋轉大絕招的物件角度
                        MagicObject_Prefab.transform.eulerAngles = new Vector3(90f, 0f, 0f);
                    }
                    else
                    {
                        //抓取滑鼠點擊到的X、Z值，Y值不用抓取
                        Target_Pos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                        //使用內插法方式讓玩家從A點轉到B點
                        Look_Pos = Vector3.Lerp(Look_Pos, Target_Pos, Time.deltaTime * 10);
                        //LookAt面相(玩家注視位置點)
                        transform.LookAt(Look_Pos);
                        //持續做普攻的動畫
                        PlayerAimator.SetBool("Att", true);
                    }
                }
            }
        }
        //如果放開滑鼠
        if (Input.GetMouseButtonUp(0)) { 
            //如果CanMagic = true 大絕招啟用
            if (CanMagic)
            {
                CanMagic = false;
                //大絕招子物件的龍可以掉下
                //MagicObject_Prefab.GetComponentInChildren<Rigidbody>.useGravity =true;
                MagicObject_Prefab.GetComponentInChildren<Rigidbody>().AddForce(0, -1000, 0);
                //能量條歸0
                GameObject.Find("GM").GetComponent<GM>().Reset();
                MagicBar.fillAmount = 0;
            }else        
                 //動畫回到idle
                PlayerAimator.SetBool("Att", false);
        }
    }
    //透過動畫呼叫此function
    public void CreateFire()
    {
        Instantiate(FireObj, CreateFireObj.transform.position, CreateFireObj.transform.rotation);
    }
    public void CreateMagic()
    {
        if (MagicBar.fillAmount == 1)
            CanMagic = true;
    }
}
