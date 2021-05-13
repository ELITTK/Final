using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDrone : Enemy
{
    [Header("ÒÆ¶¯")]
    public Transform LeftPoint, RightPoint;
    public float speed;
    public bool isLeft;

    [Header("¹¥»÷")]
    public float attackCD, trackTimeMax;
    public GameObject saber;
    public VisionDetect vision;

    private Rigidbody rigidbd;

    enum Direction { FaceLeft, FaceRight}
    private Quaternion rotation;
    private float trackTimer, attackTimer;
    private bool canMove = true;
    private Direction direction;
    private Transform target;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rigidbd = GetComponent<Rigidbody>();
        if (isLeft)
        {
            direction = Direction.FaceLeft;
        }
        else
        {
            direction = Direction.FaceRight;
        }
    }

    private void FixedUpdate()
    {
        Attack();
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
            rotation = Quaternion.LookRotation(target.transform.position - gameObject.transform.position, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        else
        {
            if (direction == Direction.FaceLeft)
            {
                if (transform.position.x < LeftPoint.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    direction = Direction.FaceRight;
                }
            }
            else
            {
                if (transform.position.x > RightPoint.position.x)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    direction = Direction.FaceLeft;
                }
            }
        }
        
    }
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
}
