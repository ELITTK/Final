using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMid : MonoBehaviour
{
    public GameObject bulletPrefb;
    public Transform LeftShootPoint, MidShootPoint, RightShootPoint;

    private Animator animator;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        EventCenter.GetInstance().AddEventListener("Boss1Phase1Attack", StartAnim);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void StartAnim()
    {
        animator.SetTrigger("Play");
    }

    void BulletGenerate()
    {
        GameObject bullet1 = Instantiate(bulletPrefb);
        bullet1.transform.position = LeftShootPoint.position;
        BulletBasic bulletf1 = bullet1.GetComponentInChildren<BulletBasic>();
        bulletf1.SetTarget(player);
        bulletf1.SetSpeed();

        GameObject bullet2 = Instantiate(bulletPrefb);
        bullet2.transform.position = LeftShootPoint.position;
        BulletBasic bulletf2 = bullet2.GetComponentInChildren<BulletBasic>();
        bulletf2.SetTarget(player);
        bulletf2.SetSpeed();

        GameObject bullet3 = Instantiate(bulletPrefb);
        bullet3.transform.position = LeftShootPoint.position;
        BulletBasic bulletf3 = bullet3.GetComponentInChildren<BulletBasic>();
        bulletf3.SetTarget(player);
        bulletf3.SetSpeed();
    }
}
