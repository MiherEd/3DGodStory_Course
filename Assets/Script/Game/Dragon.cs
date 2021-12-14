using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider hit)
    {
        if(hit.GetComponent<Collider>().name == "mazu_floor")
        {
            Destroy(this.transform.parent.gameObject);
        }
        if(hit.GetComponent<Collider>().tag == "NPC"|| hit.GetComponent<Collider>().tag == "Boss")
        {
            hit.GetComponent<Monster>().BigMagicHurt();
            Destroy(this.transform.parent.gameObject);
        }        
    }
}
