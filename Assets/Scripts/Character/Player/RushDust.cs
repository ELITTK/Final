using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushDust : MonoBehaviour
{
    private ParticleSystem particle;

    public void Start()
    {
        particle = GetComponent<ParticleSystem>();
        EventCenter.GetInstance().AddEventListener<int>("��ҳ��", PlayerRushDust);
        EventCenter.GetInstance().AddEventListener("��ҳ�̽���", PlayerRushOver);
    }

    public void PlayerRushDust(int faceDir)
    {
        if (!particle.isPlaying)
        {
            particle.Play();
            if (faceDir<0) //��
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
    
    public void PlayerRushOver()
    {
        particle.Stop();
    }


}
