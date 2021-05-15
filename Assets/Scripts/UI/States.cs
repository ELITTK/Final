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
        EventCenter.GetInstance().AddEventListener<int>("�˺�", HealLost);
        EventCenter.GetInstance().AddEventListener<int>("��������", ShieldJudge);
        EventCenter.GetInstance().AddEventListener<int>("���ܻ��", ShieldAdd);
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
            EventCenter.GetInstance().EventTrigger("����");
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
            EventCenter.GetInstance().EventTrigger<int>("����ʹ�óɹ�", cost);
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
