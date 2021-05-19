using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDrone : Enemy
{
    [Header("移动")]
    public Transform LeftPoint, RightPoint;
    public float speed;

    public float XoffSetFacingLeft;//位置补正，加上这个偏移值后得到模型实际所处位置
    public float XoffSetFacingRight;

    [Header("攻击")]
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
        //攻击使用另外一个脚本FloorDroneLaser
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

        //修改房间时无人机模型会瞬移，暂时先注释掉了
        /*
        //方向(根据模型x=1是左，x=-1是右)
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

        //攻击使用另外一个脚本FloorDroneLaser
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
        //方向(根据模型x=1是左，x=-1是右)
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
