using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class States : MonoBehaviour
{
    public Image Blood, BloodBack, Shield;
    public int MaxHealth = 100, MaxShield = 100, shieldFade = 1;
    public float fadeTime = 1f;

    private int health, previousHealth, shield;
    private float healthTimer;
    private bool isShieldFade = true;
    void Start()
    {
        shield = 0;
        health = MaxHealth;
        previousHealth = health;
        HealLost(0);
        EventCenter.GetInstance().AddEventListener<int>("伤害", HealLost);
        EventCenter.GetInstance().AddEventListener<int>("护盾消耗", ShieldJudge);
        EventCenter.GetInstance().AddEventListener<int>("护盾获得", ShieldAdd);
    }

    private void FixedUpdate()
    {
        HealthBackMove();
        ShieldFadeExcute();
        Shield.fillAmount = shield / MaxShield;
    }

    private void HealLost(int cost)
    {
        previousHealth = health;
        if (health - cost > 0)
        {
            health -= cost;
        }
        else
        {
            health = 0;
            EventCenter.GetInstance().EventTrigger("死亡");
        }
        Blood.fillAmount = health / MaxHealth;
        BloodBack.fillAmount = previousHealth / MaxHealth;
        healthTimer = fadeTime;
    }

    private void HealthBackMove()
    {
        if (healthTimer > 0)
        {
            healthTimer -= Time.fixedDeltaTime;
            BloodBack.fillAmount = (health + (previousHealth - health) * (healthTimer / fadeTime)) / MaxHealth;
        }
    }

    private void ShieldCost(int cost)
    {
        shield -= cost;
    }
    public void ShieldAdd(int add)
    {
        shield += add;
    }
    public void ShieldJudge(int cost)
    {
        if (shield > cost)
        {
            isShieldFade = false;
            ShieldCost(cost);
            EventCenter.GetInstance().EventTrigger<int>("护盾使用成功", cost);
        }
    }

    private void ShieldFadeExcute()
    {
        if (isShieldFade)
        {
            if (shield - shieldFade > 0)
            {
                shield -= shieldFade;
            }
            else
            {
                shield = 0;
            }
        }
        else
        {
            isShieldFade = true;
        }
    }
}
