using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse3D : MonoBehaviour
{
    public GameObject testObject;//�����������λ�õ���Ϸ����

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")|| Input.GetKeyDown("f"))
        {
            testObject.transform.position = GetMousePosition();
        }
    }

    /// <summary>
    /// ��ȡ���λ�ã�Z�������Z��һ��
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetMousePosition()
    {
        Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Vector3 mousePos = Input.mousePosition;//���λ��
        //ת��������,�и�����z���겻��Ϊ0�����Ժ�ò�Ƶ���6ʱ�Ƚ�׼ȷ
        //float newZ = Mathf.Abs(camera.transform.position.z);
        float newZ = 6f;
        mousePos = camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, newZ));
        //����zǰ��mousePos�������ֱ����ָλ��
        mousePos.z = -2.5f;

        return mousePos;
    }
}
