using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("冲刺用变量")]
    public KeyCode dashKey;
    public bool canDash, isDash, isDirectionalDash;
    public float dashTime, dashSpeed;
    private float dashTimeLeft;

    [Header("跳跃")]
    public bool canJump;
    public float maxHoldTime, jumpForceMin;
    private float holdTime, preInputTime = 0.1f;

    [Header("一般")]
    public float speed, jumpSpeed;

    [Header("战斗")]
    public float attackCD;
    public int energeMax, healMax;

    private bool isJump, resetJumpFlag, resetJumpTimeFlag, havePressedJump;
    private Transform RespawnLocal;
    private Animator animator;
    private Rigidbody rigidbd;
    private float horizontalMove, verticalMove;
    private float attackCDTimer, jumpPressedTimer;
    private int health, energe;
    
    // Start is called before the first frame update
    void Start()
    {
        InputMgr.GetInstance().StartOrEndCheck(true);
        EventCenter.GetInstance().AddEventListener<KeyCode>("某键按下", JudgementCenter);
        EventCenter.GetInstance().AddEventListener<int>("能量获取", EnergeCharge);
        EventCenter.GetInstance().AddEventListener("死亡", Death);
        //EventCenter.GetInstance().AddEventListener("地面检测", ResetJump);
        animator = GetComponent<Animator>();
        rigidbd = GetComponent<Rigidbody>();
        RespawnLocal.position = gameObject.transform.position;
        energe = 0;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal") * -1;
        verticalMove = Input.GetAxis("Vertical");
        Jump();
        ResetJump();
        MeleeAttack();
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
        if (Mathf.Abs(horizontalMove) > 0.1f)
        {
            animator.SetBool("IsMove", true);
        }
        else
        {
            animator.SetBool("IsMove", false);
        }
    }

    private IEnumerator DelayTime(float time)
    {
        yield return new WaitForSeconds(time);
    }

    /// <summary>
    /// 为监听执行的处理中心
    /// </summary>
    /// <param name="key">对应的按键</param>
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
                EventCenter.GetInstance().EventTrigger<int>("玩家冲刺",(int)transform.localScale.x);
            }
            else
            {
                EventCenter.GetInstance().EventTrigger("玩家冲刺结束");
            }
        }
    }

    /// <summary>
    /// 根据按下的时长度跳跃的高度有所不同
    /// </summary>
    private void Jump()
    {
        if (canJump)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (holdTime < maxHoldTime)
                {
                    resetJumpTimeFlag = false;
                    holdTime += Time.deltaTime;
                    rigidbd.velocity = new Vector3(rigidbd.velocity.x, jumpForceMin, 0);
                    EventCenter.GetInstance().EventTrigger("玩家跳跃");
                }
                else if (!resetJumpTimeFlag)
                {
                    resetJumpTimeFlag = true;
                    canJump = false;
                    //Debug.Log("1");
                }
            }
            else if (havePressedJump == true)
            {
                havePressedJump = false;
                rigidbd.velocity = new Vector3(rigidbd.velocity.x, jumpForceMin, 0);
                resetJumpTimeFlag = true;
                canJump = false;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (holdTime < maxHoldTime)
                {
                    canJump = false;
                    //Debug.Log("2");
                }
                holdTime = 0;
                
            }
        }
        else {
            if (Input.GetKey(KeyCode.Space))
            {
                havePressedJump = true;
                jumpPressedTimer = preInputTime;
            }
        }
        if (jumpPressedTimer > 0)
        {
            jumpPressedTimer -= Time.deltaTime;
            if (jumpPressedTimer < 0)
            {
                havePressedJump = false;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("BulletEnemy"))
        {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            EventCenter.GetInstance().EventTrigger<int>("伤害", bullet.damage);
        }
        else if (other.gameObject.CompareTag("RespawnPoint"))
        {
            RespawnLocal.position = transform.position;
        }
    }

    private void ResetJump()
    {
        if(rigidbd.velocity.y < -0.1f && ! resetJumpFlag)
        {
            resetJumpFlag = true;
            //Debug.Log("true");
        }
        if (rigidbd.velocity.y >= -0.1f && resetJumpFlag)
        {
            //Debug.Log("false");
            resetJumpFlag = false;
            canJump = true;
        }
    }

    private void MeleeAttack()
    {
        if (attackCDTimer < attackCD)
        {
            attackCDTimer += Time.deltaTime;
            animator.SetBool("IsAttack", false);
        }
        else
        {
            if (Input.GetMouseButtonDown(1))
            {
                animator.SetBool("IsAttack", true);
                attackCDTimer = 0;
            }
        }
    }

    private void EnergeCharge(int energeNum)
    {
        if (energe + energeNum > energeMax)
        {
            energe = energeMax;
        }
        else
        {
            energe += energeNum;
        }
    }
    private void Death()
    {
        transform.position = RespawnLocal.position;

    }
    public void Damage(int dmg)
    {
        EventCenter.GetInstance().EventTrigger<int>("伤害", dmg);
    }

    public void RespawnSet()
    {
        RespawnLocal.position = transform.position;
    }
}