using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDroneLaser : BaseLaser
{
    public FloorDroneAtkRange atkRange;
    public Transform[] laserStartPoints;
    private int randomInt = 0;

    protected override void Start()
    {
        base.Start();
    }

    protected override Vector3 Shoot_GetShootDir()
    {
        Vector3 dir = atkRange.getDetected().position - laserStartPoints[randomInt].position;
        return dir;
    }

    protected override void DealDmg(RaycastHit hit)
    {
        //对玩家造成伤害
        /*
        PlayerMovement playerMovement = hit.collider.gameObject.GetComponentInParent<PlayerMovement>();
        if (playerMovement)
        {
            float dmg = DmgPerSecond * Time.deltaTime;
            Debug.Log("射线每帧伤害：" + dmg);
            playerMovement.TakeDmg(dmg);
        }
        */
    }

    protected override bool SetIsShootingTrue()
    {
        return atkRange.isFoundTarget();
    }

    protected override bool SetIsShootingFalse()
    {
        return !atkRange.isFoundTarget();
    }

    protected override Vector3 GetLaserStartPoint()
    {
        randomInt = Random.Range(0, 2);
        return laserStartPoints[randomInt].position;
    }
}
