using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBasic : Bullet
{
    public float speed, timeLimit;
    public GameObject startPoint, endPoint;

    private static BulletBasic instance;
    private Vector3 direction;
    private float time;
    private void Start()
    {
        gameObject.transform.position = startPoint.transform.position;
    }

    private void OnEnable()
    {
        gameObject.transform.position = startPoint.transform.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (time < timeLimit)
        {
            DirectionSet();
            time += Time.deltaTime;
        }
        gameObject.transform.position += direction * speed; 
    }

    private void DirectionSet()
    {
        direction = endPoint.transform.position - startPoint.transform.position;
        direction = direction.normalized;
    }

    /*放到bulletScript里了
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletEndPoint"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("Player"))
        {
            //Debug.Log("111");
            other.gameObject.GetComponent<PlayerMovement>().Damage(damage);
            Destroy(gameObject);
        }
    }*/

    public void SetTarget(Transform tar)
    {
        endPoint.transform.position = tar.transform.position;
    }

    public void SetSpeed(float sp = 0.15f)
    {
       speed = sp;
    }
}
