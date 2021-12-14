using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("�ƹ��I�쪺�y�Ц�m")]
    public Vector3 Target_Pos;
    [Header("���a�`����V")]
    public Vector3 Look_Pos;
    [Header("�����g�u���쪺����")]
    public RaycastHit[] hits;
    RaycastHit hit;
    [Header("���a�ʧ@")]
    public Animator PlayerAimator;
    [Header("���a���𪫥�")]
    public GameObject FireObj;
    [Header("���𪫥�ͦ�����m")]
    public GameObject CreateFireObj;

    public bool CanMagic;
    [Header("�j���۪���")]
    public GameObject MagicObject;
    //�x�s�j����Prefab����
    GameObject MagicObject_Prefab;
    [Header("�I�񵴩۱�")]
    public Image MagicBar;
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* �g�u������Ӥ覡 
         * �@�ӬO Raycast:
         * �o�g�X�� �I�쪫�� �N�|����
         * �t�@�جO RaycastAll:
         **/
        //Input.GetMouseButton
        //Input.GetMouseButtonDown �I�@����@��
        //Input.GetMouseButtonUp ��}�~Ĳ�o
        //0:�ƹ����� 1:�ƹ��k�� 2:�����u��
        if (Input.GetMouseButton(0))
        {
            //���ƹ��I����3����m�M�D��v����3���y�Ц�m�s���@�u->�g�u
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;
            //�g�u���쪺�Ҧ�Collider������Ȧs�bhits�}�C��
            hits = Physics.RaycastAll(ray, 100);
            /*
            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.red);
                if (hit.collider.name == "mazu_floor")
                {
                    //����ƹ��I���쪺X�BZ�ȡAY�Ȥ��Χ��
                    Target_Pos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                    //�ϥΤ����k�覡�����a�qA�I���B�I
                    Look_Pos = Vector3.Lerp(Look_Pos, Target_Pos, Time.deltaTime * 10);
                    //LookAt����(���a�`����m�I)
                    transform.LookAt(Look_Pos);
                }
            }*/
            //�ϥ�for�j��P�_hits�}�C�����S���a�O�o�Ӫ���
            for(int i = 0; i< hits.Length; i++)
            {
                hit = hits[i];
                if (hit.collider.name == "mazu_floor")
                {
                    //CanMagic = true �i�H�ϥΤj����
                    if (CanMagic)
                    {
                        if (GameObject.FindGameObjectsWithTag("Magic").Length <= 0)
                            MagicObject_Prefab = Instantiate(MagicObject, transform.position, transform.rotation) as GameObject;

                        //�j���۪������m = �g�u����a�O����m
                        MagicObject_Prefab.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                        //����j���۪����󨤫�
                        MagicObject_Prefab.transform.eulerAngles = new Vector3(90f, 0f, 0f);
                    }
                    else
                    {
                        //����ƹ��I���쪺X�BZ�ȡAY�Ȥ��Χ��
                        Target_Pos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                        //�ϥΤ����k�覡�����a�qA�I���B�I
                        Look_Pos = Vector3.Lerp(Look_Pos, Target_Pos, Time.deltaTime * 10);
                        //LookAt����(���a�`����m�I)
                        transform.LookAt(Look_Pos);
                        //���򰵴��𪺰ʵe
                        PlayerAimator.SetBool("Att", true);
                    }
                }
            }
        }
        //�p�G��}�ƹ�
        if (Input.GetMouseButtonUp(0)) { 
            //�p�GCanMagic = true �j���۱ҥ�
            if (CanMagic)
            {
                CanMagic = false;
                //�j���ۤl�����s�i�H���U
                //MagicObject_Prefab.GetComponentInChildren<Rigidbody>.useGravity =true;
                MagicObject_Prefab.GetComponentInChildren<Rigidbody>().AddForce(0, -1000, 0);
                //��q���k0
                GameObject.Find("GM").GetComponent<GM>().Reset();
                MagicBar.fillAmount = 0;
            }else        
                 //�ʵe�^��idle
                PlayerAimator.SetBool("Att", false);
        }
    }
    //�z�L�ʵe�I�s��function
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
