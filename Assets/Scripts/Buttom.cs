using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttom : MonoBehaviour
{
    private int isAttacked = 0;
    private GameObject[] buttoms;

    void start()
    {
        buttoms = GameObject.FindGameObjectsWithTag("buttom");
    }

    void onCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "BulletPlayer")
        {
            isAttacked += 1;
        }
        if(isAttacked>=3)
        {
            foreach(GameObject sum in buttoms)
            {
                DestroyImmediate(sum);
            }
            
        }
    }
}
