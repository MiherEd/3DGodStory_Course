using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    [Header("���ʳt��")]
    public float Speed;
    [Header("�]���`��q")]
    public float TotalHP;
    float ScriptHP;
    [Header("�Q���𥴨���˪��ƭ�")]
    public float HurtHP;
    [Header("�Q���𥴨��h���Z����")]
    public float HurtDis;
    //�{���������ʳt��
    float ScriptSpeed;
    // Start is called before the first frame update
    void Start()
    {
        ScriptSpeed = Speed;
        ScriptHP = TotalHP;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * ScriptSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.GetComponent<Collider>().name == "mazu_wall")
        {
            ScriptSpeed = 0;
            GetComponent<Animator>().SetBool("Attack", true);
        }
    }
    private void OnTriggerExit(Collider hit)
    {
        if (hit.GetComponent<Collider>().name == "mazu_wall")
        {
            ScriptSpeed = Speed;
            GetComponent<Animator>().SetBool("Attack", false);
        }
    }
    public void Hurt()
    {
        ScriptHP -= HurtHP;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - HurtDis);
        if (ScriptHP <= 0)
        {
            ScriptSpeed = 0;
            GetComponent<Collider>().enabled = false;
            GetComponent<Animator>().SetTrigger("Die");
            if (gameObject.tag == "NPC")
                GameObject.Find("GM").GetComponent<GM>().MonsterDie();   
            else
                GameObject.Find("GM").GetComponent<GM>().GameOver();
        }
    }
    public void AttPlayer()
    {
        GameObject.Find("GM").GetComponent<GM>().HurtPlayer();
    }
    public void BossAttPlayer()
    {
        GameObject.Find("GM").GetComponent<GM>().BossHurtPlayer();
    }
    public void BigMagicHurt()
    {
        ScriptHP -= HurtHP * 100;
        if(ScriptHP <= 0)
        {
            ScriptSpeed = 0;
            GetComponent<Collider>().enabled = false;
            GetComponent<Animator>().SetTrigger("Die");
            GameObject.Find("GM").GetComponent<GM>().Score();

            if (gameObject.tag == "NPC")
                GameObject.Find("GM").GetComponent<GM>().MonsterDie();
            else
                GameObject.Find("GM").GetComponent<GM>().GameOver();
        }
    }
}
