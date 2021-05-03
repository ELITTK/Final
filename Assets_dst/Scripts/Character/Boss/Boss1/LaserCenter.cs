using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCenter : MonoBehaviour
{
    public GameObject[] LaserTypeStorage = new GameObject[5];
    private int currentType;
    private float timer, laserTime;
    private bool isLaser;
    // Start is called before the first frame update
    void Start()
    {
        EventCenter.GetInstance().AddEventListener<int>("Boss2Phase1Attack", LaserExcute);
        laserTime = LaserTypeStorage[0].GetComponentInChildren<Laser>().GetTime();
    }

    private void FixedUpdate()
    {
        LaserDisable(currentType);
        
    }
    void LaserExcute(int type)
    {
        isLaser = true;
        currentType = type;
        LaserTypeStorage[type].SetActive(true);
    }

    void LaserDisable(int type)
    {
        if (isLaser)
        {
            timer += Time.deltaTime;
            if (timer > laserTime)
            {
                LaserTypeStorage[type].SetActive(false);
                timer = 0;
                isLaser = false;
            }
        }
    }
}
