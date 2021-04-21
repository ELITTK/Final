using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse3D : MonoBehaviour
{
    public GameObject testObject;//用来测试鼠标位置的游戏物体

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")|| Input.GetKeyDown("f"))
        {
            testObject.transform.position = GetMousePosition();
        }
    }

    /// <summary>
    /// 获取鼠标位置，Z轴与玩家Z轴一致
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetMousePosition()
    {
        Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Vector3 mousePos = Input.mousePosition;//鼠标位置
        //转世界坐标,有个坑是z坐标不能为0，尝试后貌似等于6时比较准确
        //float newZ = Mathf.Abs(camera.transform.position.z);
        float newZ = 6f;
        mousePos = camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, newZ));
        //设置z前，mousePos还是鼠标直接所指位置
        mousePos.z = -2.5f;

        return mousePos;
    }
}
