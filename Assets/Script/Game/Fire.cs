using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [Header("���ʳt��")]
    public float Speed;
    [Header("Fire��������ɶ�")]
    public float DeleteTime;
    // Start is called before the first frame update
    void Start()
    {
        //�R������
        Destroy(gameObject, DeleteTime);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.GetComponent<Collider>().tag == "NPC"|| hit.GetComponent<Collider>().tag == "Boss")
        {
            hit.GetComponent<Monster>().Hurt();
            Destroy(gameObject);
        }
    }
}
