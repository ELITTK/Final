using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public int chargeAmountMin, chargeAmountMax;

    protected Animator animator;
    protected float MaxHealth;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        MaxHealth = health;
    }

    public virtual void takeDmg(float dmg)
    {
        if (health>0)
        {
            health -= dmg;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    public virtual int GetCharge()
    {
        return Random.Range(chargeAmountMin, chargeAmountMax);
    }

    protected virtual void Die()
    {
        //Anim
        DestroyGo();
    }

    protected virtual void DestroyGo()
    {
        EventCenter.GetInstance().EventTrigger<int>("能量获取", GetCharge());
        Destroy(gameObject);
    }
}
