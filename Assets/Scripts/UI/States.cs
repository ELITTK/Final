using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class States : MonoBehaviour
{
    [Header("Ѫ��")]
    public Text healText;
    public int maxHealth;
    private int health;
    void Start()
    {
        health = maxHealth;
        HealLost(0);
        EventCenter.GetInstance().AddEventListener<int>("�˺�", HealLost);
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
            EventCenter.GetInstance().EventTrigger("����");
        }
        healText.text = health.ToString();
    }
}
