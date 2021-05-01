using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;

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

    protected virtual void Die()
    {
        //Anim
        DestroyGo();
    }

    protected virtual void DestroyGo()
    {
        Destroy(gameObject);
    }
}
