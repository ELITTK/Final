using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sk0_FireBall : BaseShoot
{
    //覆盖，返回射击方向
    protected override Vector3 Shoot_GetShootDir()
    {
        Vector3 mousePos = Mouse3D.GetMousePosition();

        Vector3 v = mousePos - new Vector3 (firePoint.position.x, firePoint.position.y, 0);

        return v;
    }
}
