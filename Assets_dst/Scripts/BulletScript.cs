using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletDmg;
    
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
