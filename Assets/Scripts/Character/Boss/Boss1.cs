using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss1 : Enemy
{
    public Transform BossLeftPosition, BossMidPosition, BossRightPosition;
    public float damage1, phase1CD;
    public GameObject FireMid, Ui;
    public Image healthBar;
    public float phase2CD;
    public int LaserNum;

    private Vector3[] BossPosition = new Vector3[3];
    private int bossPositionIndex;

    private int currentLaserType;

    private int currentPhase = 1;
    private float TimeController1, TimeController2;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Ui.SetActive(true);
        BossPosition[0] = BossRightPosition.position;
        BossPosition[1] = BossMidPosition.position;
        BossPosition[2] = BossLeftPosition.position;
    }

    private void FixedUpdate()
    {
        PhaseController();
        UIController();
    }

    void PhaseController()
    {
        if (health > MaxHealth * 2 / 3)
        {
            Phase1();
        }
        else if (health > MaxHealth / 3)
        {
            if (currentPhase == 1)
            {
                currentPhase = 2;
                TimeController1 = TimeController2 = 0;
            }
            
            Phase2();
        }
        else
        {
            if (currentPhase == 2)
            {
                currentPhase = 3;
                TimeController1 = TimeController2 = 0;
            }
            Phase1();
            Phase2();
        }
    }
    void Phase1()
    {
        TimeController1 += Time.deltaTime;
        if (TimeController1 > phase1CD)
        {
            EventCenter.GetInstance().EventTrigger("Boss1Phase1Attack");
            TimeController1 = 0;
        }
        else if (TimeController1 >= phase1CD - Time.deltaTime)
        {
            bossPositionIndex++;
            bossPositionIndex %= 3;
            transform.position = BossPosition[bossPositionIndex];
            //插入一段移动动画
        }
    }
    void Phase2()
    {
        TimeController2 += Time.deltaTime;
        if (TimeController2 > phase2CD)
        {
            EventCenter.GetInstance().EventTrigger<int>("Boss2Phase1Attack", currentLaserType % LaserNum);
            currentLaserType++;
            currentLaserType %= LaserNum;
            TimeController2 = 0;
        }
    }
    private void UIController()
    {
        healthBar.fillAmount = health * 1f / MaxHealth;
    }
    private void OnDestroy()
    {
        UIController();
    }
}
