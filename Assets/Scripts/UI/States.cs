using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class States : MonoBehaviour
{
    [Header("ÑªÁ¿")]
    public Text healText;
    public int maxHealth;
    private int health;
    void Start()
    {
        health = maxHealth;
        HealLost(0);
        EventCenter.GetInstance().AddEventListener<int>("ÉËº¦", HealLost);
    }

    private void HealLost(int cost)
    {
        if (health - cost > 0)
        {
            health -= cost;
        }
        else
        {
            health = 0;
            EventCenter.GetInstance().EventTrigger("ËÀÍö");
        }
        healText.text = health.ToString();
    }
}
