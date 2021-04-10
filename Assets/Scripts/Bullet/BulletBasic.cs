using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBasic : Bullet
{
    public float speed;
    public GameObject startPoint, endPoint;

    private static BulletBasic instance;
    private Vector3 direction;
    private void Start()
    {
        instance = this;
        instance.transform.position = startPoint.transform.position;
        DirectionSet();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        instance.transform.position += direction * speed; 
    }

    private void DirectionSet()
    {
        direction = endPoint.transform.position - startPoint.transform.position;
        direction = direction.normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BulletEndPoint")
        {
            Destroy(instance);
        }
    }
}
