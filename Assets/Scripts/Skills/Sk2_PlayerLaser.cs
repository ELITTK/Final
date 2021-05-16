using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sk2_PlayerLaser : MonoBehaviour
{
    public KeyCode ExecuteKey;
    public LineRenderer laser;
    private bool isShooting = false;

    private void Update()
    {
        if (Input.GetKeyDown(ExecuteKey))
        {
            isShooting = true;
        }
        else if (Input.GetKeyUp(ExecuteKey))
        {
            isShooting = false;
            CloseLaser();
        }

        if (isShooting)
        {
            OpenLaser();
        }
    }

    private void OpenLaser()
    {
        //创建射线
        Vector3 dir = Shoot_GetShootDir();

        RaycastHit hit;

        bool isHit = Physics.Raycast(transform.position, dir, out hit, 300);
        Debug.DrawLine(transform.position,hit.point);

        //LineRenderer
        laser.enabled = true;
        laser.SetPosition(0, transform.position);
        if (isHit)
        {
            laser.SetPosition(1, hit.point);
        }
        else
        {
            laser.SetPosition(1, transform.position + dir * 50);
        }
    }

    private void CloseLaser()
    {
        laser.enabled = false;
    }

    //覆盖，返回射击方向
    protected Vector3 Shoot_GetShootDir()
    {
        Vector3 mousePos = Mouse3D.GetMousePosition();

        Vector3 v = mousePos - new Vector3(transform.position.x, transform.position.y, 0);

        v.z = 0;

        return v;
    }

}
