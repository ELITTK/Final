using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private BoxCollider box;
    // Start is called before the first frame update
    private void Start()
    {
        box = gameObject.GetComponent<BoxCollider>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Ground!");
        if (collision.gameObject.CompareTag("Environment"))
        {
            EventCenter.GetInstance().EventTrigger("地面检测");
        }
        
    }
    /*private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Environment"))
        {
            if (isGround)
            {
                isGround = false;
                EventCenter.GetInstance().EventTrigger<bool>("地面检测", isGround);
            }
        }
    }*/
}
