using System.Collections.Generic;
using UnityEngine;

public class Drone : Enemy
{
    [Header("�ƶ�")]
    public float distenceMin, speed;

    [Header("���")]
    public float shootCD, bulletSpeed;
    public Transform ShootPoint;//�ӵ�����λ��

    private Rigidbody rigidbd;
    private Transform player;

    private Quaternion rotation;
    private float time;
    private bool canMove = true;

    [Header("�����")]
    public GameObject bulletPrefab;
    public static List<GameObject> bulletPoolList = new List<GameObject>();//�����,�������˻�������������

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
            GameObject bullet = PopPool();
            bullet.transform.position = ShootPoint.position;
            //bullet.transform.position = gameObject.GetComponentInParent<Transform>().position;
            BulletBasic bulletf = bullet.GetComponentInChildren<BulletBasic>();
            bulletf.SetTarget(player);
            bulletf.SetSpeed(bulletSpeed);
            time = 0;
            bullet.SetActive(true);

        }

    }


    /// <summary>
    /// �Ӷ������ȡ��һ���ӵ������û����������һ��
    /// </summary>
    /// <returns></returns>
    public GameObject PopPool()
    {
        foreach (GameObject obj in bulletPoolList)
        {
            if (obj.activeSelf == false)
            {
                return obj;
            }
        }
        GameObject bullet = Instantiate(bulletPrefab);
        bulletPoolList.Add(bullet);

        return bullet;
    }
}
