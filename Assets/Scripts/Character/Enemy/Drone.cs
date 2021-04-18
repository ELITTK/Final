using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : Enemy
{
    [Header("�ƶ�")]
    public float distenceMin, speed;
    public GameObject bulletPrefb;

    [Header("���")]
    public float shootCD, bulletSpeed;

    private Rigidbody rigidbd;
    private Transform player;

    private Quaternion rotation;
    private float time;
    private bool canMove = true;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rigidbd = GetComponent<Rigidbody>();
        
    }

    private void FixedUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Distence();
        Shoot();
        if (canMove)
        {
            Movement();
        }
    }
    /// <summary>
    /// ��������ҵľ��룬��һ����Χ��ʱ�Զ�ֹͣ
    /// </summary>
    private void Distence()
    {
        float sqrLenght = (player.position - transform.position).sqrMagnitude;
        if (sqrLenght < distenceMin * distenceMin)
        {
            canMove = false;
        }
    }
    private void Movement()
    {
        rotation = Quaternion.LookRotation(player.transform.position - gameObject.transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
    private void Shoot()
    {
        time += Time.deltaTime;
        if (time > shootCD)
        {
            GameObject bullet = Instantiate(bulletPrefb);
            bullet.transform.position = transform.position;
            BulletBasic bulletf = bullet.GetComponentInChildren<BulletBasic>();
            bulletf.SetTarget(player);
            bulletf.SetSpeed(bulletSpeed);
            time = 0;
        }
        
    }
}
