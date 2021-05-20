using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLaser : MonoBehaviour
{
    public LineRenderer laser;
    private bool isShooting = false;

    public GameObject hitEffect;
    private ParticleSystem[] Effects;

    public float DmgPerSecond = 30;

    protected virtual void Start()
    {
        Effects = GetComponentsInChildren<ParticleSystem>();
    }

    protected void Update()
    {
        if (SetIsShootingTrue())
        {
            isShooting = true;
        }
        else if(SetIsShootingFalse())
        {
            isShooting = false;
            CloseLaser();
        }

        if (isShooting)
        {
            OpenLaser();
        }
    }

    protected void OpenLaser()
    {
        //创建射线
        Vector3 laserStartPoint = GetLaserStartPoint();
        Vector3 dir = Shoot_GetShootDir();

        RaycastHit hit;

        bool isHit = Physics.Raycast(laserStartPoint, dir, out hit, 300);
        //Debug.DrawLine(transform.position, hit.point);

        //LineRenderer
        laser.enabled = true;
        laser.SetPosition(0, laserStartPoint);
        if (isHit)
        {
            laser.SetPosition(1, hit.point);
            //Debug.Log("射线命中：" + hit.collider.gameObject.name);
        }
        else
        {
            laser.SetPosition(1, transform.position + dir * 50);
        }

        //hitEffect
        hitEffect.transform.position = hit.point;
        hitEffect.transform.rotation = Quaternion.identity;
        if (isHit)
        {
            foreach (var AllPs in Effects)
            {
                if (!AllPs.isPlaying) AllPs.Play();
            }
        }
        else
        {
            DisableEffect();
        }

        //伤害
        DealDmg(hit);
    }

    protected void CloseLaser()
    {
        laser.enabled = false;

        //effect
        DisableEffect();
    }



    //覆盖，返回射击方向
    protected virtual Vector3 Shoot_GetShootDir()
    {
        Vector3 mousePos = Mouse3D.GetMousePosition();

        Vector3 v = mousePos - new Vector3(transform.position.x, transform.position.y, 0);

        v.z = 0;

        return v;
    }

    protected virtual void DealDmg(RaycastHit hit)
    {
        Enemy enemyScript = hit.collider.gameObject.GetComponentInParent<Enemy>();
        if (enemyScript)
        {
            float dmg = DmgPerSecond * Time.deltaTime;
            Debug.Log("射线每帧伤害：" + dmg);
            enemyScript.takeDmg(dmg);
        }
    }

    protected virtual bool SetIsShootingTrue()
    {
        return false;
    }
    protected virtual bool SetIsShootingFalse()
    {
        return false;
    }

    protected virtual Vector3 GetLaserStartPoint()
    {
        return transform.position;
    }

    private void DisableEffect()
    {
        ParticleSystem[] Hit = hitEffect.GetComponentsInChildren<ParticleSystem>();
        foreach (var AllPs in Hit)
        {
            if (AllPs.isPlaying) AllPs.Stop();
        }
    }

}
