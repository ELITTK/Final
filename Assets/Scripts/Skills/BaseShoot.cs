using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShoot : BaseSkillCaster
{
    [Header("射击")]
    public Transform firePoint;//开火点
    public GameObject bulletPrefab;//射出子弹类型
    public float bulletDmg = 30;//子弹伤害
    public float bulletForce = 20.0f;//子弹初速度

    [HideInInspector]
    public List<GameObject> poolList;//对象池简易版 暂时没用框架


    public override void ExcuteSkill()
    {
        Shoot();
    }

    public void Shoot()//开火！！！
    {
        Shoot_ShootStart();//射击开始事件

        Vector3 shootDir = Shoot_GetShootDir();//设置开火方向

        shootDir.z = 0;
        shootDir.Normalize();

        //生成子弹
        GameObject bullet = PopPool();
        bullet.transform.position = firePoint.position;
        bullet.SetActive(true);

        /*
        if (shootDir.x < 0)//子弹左右方向
        {
            bullet.transform.localScale = new Vector3(-1 * bullet.transform.localScale.x, bullet.transform.localScale.y, bullet.transform.localScale.z);
        }*/

        //设置子弹信息
        Shoot_SetBulletInfo(bullet);

        //发射子弹
        Shoot_LauchBullet(bullet, shootDir);

        //Shoot_ShootOver();
    }


    public GameObject PopPool()
    {
        foreach (GameObject obj in poolList)
        {
            if (obj.activeSelf == false)
            {
                return obj;
            }
        }
        GameObject bullet = Instantiate(bulletPrefab);
        poolList.Add(bullet);

        return bullet;
    }

    protected virtual void Shoot_ShootStart()//射击开始时的事件，在子类里覆盖
    {

    }

    protected virtual Vector3 Shoot_GetShootDir()//返回射击方向，在子类里覆盖
    {
        return firePoint.position - transform.position;//开火方向
    }


    protected virtual void Shoot_ShootEveryBullet() //发射每发子弹时触发的事件 在子类里覆盖
    {

    }

    protected virtual void Shoot_SetBulletInfo(GameObject bullet) //设置子弹信息 在子类里覆盖
    {
        BulletScript bulletScript = bullet.GetComponent<BulletScript>();
        float weaponNowDmg = bulletDmg;
        bulletScript.bulletDmg = (int)(weaponNowDmg * Random.Range(0.9f, 1.1f));
    }
    protected virtual void Shoot_ShootOver()//射击结束的事件，在子类里覆盖
    {

    }

    protected void Shoot_LauchBullet(GameObject bullet, Vector3 shootDir)
    {
        //子弹发射
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0,0,0);
        rb.AddForce(shootDir * bulletForce, ForceMode.Impulse);
    }

}

