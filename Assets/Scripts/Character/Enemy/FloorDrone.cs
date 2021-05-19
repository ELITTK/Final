using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDrone : Enemy
{
    [Header("�ƶ�")]
    public Transform LeftPoint, RightPoint;
    public float speed;

    public float XoffSetFacingLeft;//λ�ò������������ƫ��ֵ��õ�ģ��ʵ������λ��
    public float XoffSetFacingRight;

    [Header("����")]
    public float attackCD, trackTimeMax;
    public GameObject saber;
    public VisionDetect vision;

    private Rigidbody rigidbd;

    private Quaternion rotation;
    private float trackTimer, attackTimer;
    private bool canMove = true;
    private Transform target;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rigidbd = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //����ʹ������һ���ű�FloorDroneLaser
        //Attack();
        if (canMove)
        {
            Movement();
        }
    }
    private void Movement()
    {
        if (vision.isFoundTarget())
        {
            trackTimer = trackTimeMax;
            target = vision.getDetected();
        }
        if (trackTimer > 0)
        {
            trackTimer -= Time.deltaTime;
            //rotation = Quaternion.LookRotation(target.transform.position - gameObject.transform.position, Vector3.up);
            //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
            transform.Translate(new Vector3(transform.localScale.x, 0, 0) * Time.deltaTime * speed);
        }

        //�޸ķ���ʱ���˻�ģ�ͻ�˲�ƣ���ʱ��ע�͵���
        /*
        //����(����ģ��x=1����x=-1����)
        if (isFacingLeft())
        {
            if (GetPosX() > target.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                //transform.position+=new Vector3()
            }
        }
        else
        {
            if (GetPosX() < target.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        */

        //����ʹ������һ���ű�FloorDroneLaser
        /*
        private void Attack()
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attackTimer = attackCD;
                //add attack movement
            }
        }
        */
    }

    private bool isFacingLeft()
    {
        //����(����ģ��x=1����x=-1����)
        return transform.localScale.x > 0;
    }

    private float GetPosX()
    {
        if (isFacingLeft())
        {
            return transform.position.x + XoffSetFacingLeft;
        }
        else
        {
            return transform.position.x + XoffSetFacingRight;
        }
    }
}
