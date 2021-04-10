using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sk1_FastShoot : BaseShoot
{
    private Transform cameraTransform;

    public void Awake()
    {
        cameraTransform = GameObject.Find("Main Camera").transform;
    }

    public void Start()
    {
        Shoot();
    }


    //覆盖，返回射击方向
    protected override Vector3 Shoot_GetShootDir()
    {
        Vector3 mousePos = Input.mousePosition;//鼠标位置
        //转世界坐标,有个坑是z坐标不能为0，很烦,好像使用摄像机的z绝对值才比较准确
        float newZ = Mathf.Abs(cameraTransform.position.z);
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, newZ));

        Vector3 v = mousePos - new Vector3(firePoint.position.x, firePoint.position.y, 0);
        Debug.Log("鼠标坐标" + mousePos.ToString());
        Debug.Log("开火点坐标" + firePoint.transform.position.ToString());
        Debug.Log("两者差值" + v.ToString());

        firePoint.transform.rotation = Quaternion.Euler(v);

        return v;
    }
}
