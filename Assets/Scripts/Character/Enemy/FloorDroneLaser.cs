using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDroneLaser : BaseLaser
{
    //private FloorDrone floorDrone;
    public FloorDrone floorDrone;
    public Transform[] laserStartPoints;
    private int randomInt = 0;

    protected override void Start()
    {
        base.Start();

        //floorDrone = GetComponentInParent<FloorDrone>();
    }

    protected override Vector3 Shoot_GetShootDir()
    {
        //return new Vector3(transform.localScale.x, 0, 0);
        Vector3 dir = floorDrone.vision.getDetected().position- laserStartPoints[randomInt].position;
        return dir;
    }

    protected override void DealDmg(RaycastHit hit)
    {
        //…À∫¶ÕÊº“
        /*
        PlayerMovement playerMovement = hit.collider.gameObject.GetComponentInParent<PlayerMovement>();
        if (playerMovement)
        {
            float dmg = DmgPerSecond * Time.deltaTime;
            Debug.Log("…‰œﬂ√ø÷°…À∫¶£∫" + dmg);
            playerMovement.takeDmg(dmg);
        }
        */
    }

    protected override bool SetIsShootingTrue()
    {
        return floorDrone.vision.isFoundTarget();
    }

    protected override bool SetIsShootingFalse()
    {
        return ! floorDrone.vision.isFoundTarget();
    }

    protected override Vector3 GetLaserStartPoint()
    {
        randomInt = Random.Range(0, 2);
        return laserStartPoints[randomInt].position;
    }
}
