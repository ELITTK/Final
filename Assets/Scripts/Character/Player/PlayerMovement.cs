using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("����ñ���")]
    public KeyCode dashKey;
    public bool canDash, isDash;
    public float dashTime, dashSpeed;
    private float dashTimeLeft;
    [Header("һ��")]
    public float speed;

    

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

        GroundMove();

    }
    private void FixedUpdate()
    {
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
        if (dashTimeLeft > 0)
        {
            dashTimeLeft -= Time.deltaTime;
            rigidbd.velocity = new Vector3(horizontalMove * dashSpeed, verticalMove * dashSpeed, 0);
        }
    }
}
