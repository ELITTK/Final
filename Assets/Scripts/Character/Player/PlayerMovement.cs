using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("����ñ���")]
    public KeyCode dashKey;
    public bool canDash, isDash, isDirectionalDash;
    public float dashTime, dashSpeed;
    private float dashTimeLeft;

    [Header("��Ծ")]
    public bool canJump;
    public float maxHoldTime, jumpForceMin;
    private float holdTime;

    [Header("һ��")]
    public float speed, jumpSpeed;

    private bool isJump;
    private Animator animator;
    private Rigidbody rigidbd;
    private float horizontalMove, verticalMove;
    
    // Start is called before the first frame update
    void Start()
    {
        InputMgr.GetInstance().StartOrEndCheck(true);
        EventCenter.GetInstance().AddEventListener<KeyCode>("ĳ������", JudgementCenter);
        animator = GetComponent<Animator>();
        rigidbd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal") * -1;
        verticalMove = Input.GetAxis("Vertical");

        if (canJump)
        {
            Jump();
        }
    }
    private void FixedUpdate()
    {
        GroundMove();
        AnimControl();
        if (canDash)
        {
            DashExcuting();
        }
        
    }
    void GroundMove()
    {
        rigidbd.velocity = new Vector3(horizontalMove * speed, rigidbd.velocity.y, 0);
    }
    void AnimControl()
    {
        if (horizontalMove > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalMove < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private IEnumerator DelayTime(float time)
    {
        yield return new WaitForSeconds(time);
    }

    /// <summary>
    /// Ϊ����ִ�еĴ�������
    /// </summary>
    /// <param name="key">��Ӧ�İ���</param>
    private void JudgementCenter(KeyCode key)
    {
        if (key == dashKey)
        {
            dashTimeLeft = dashTime;
        }
    }

    private void DashExcuting()
    {
        if (!isDirectionalDash)
        {
            if (dashTimeLeft > 0)
            {
                dashTimeLeft -= Time.deltaTime;
                rigidbd.velocity = new Vector3(horizontalMove * dashSpeed, verticalMove * dashSpeed, 0);
            }
        }
    }

    /// <summary>
    /// ���ݰ��µ�ʱ������Ծ�ĸ߶�������ͬ
    /// </summary>
    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (holdTime < maxHoldTime)
            {
                holdTime += Time.deltaTime;
                rigidbd.velocity = new Vector3(rigidbd.velocity.x, jumpForceMin, 0);
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            holdTime = 0;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            EventCenter.GetInstance().EventTrigger<int>("�˺�", bullet.damage);
        }
    }
}
