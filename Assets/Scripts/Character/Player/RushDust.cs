using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushDust : MonoBehaviour
{
    private ParticleSystem particle;

    public void Start()
    {
        particle = GetComponent<ParticleSystem>();
        EventCenter.GetInstance().AddEventListener<int>("Íæ¼Ò³å´Ì", PlayerRushDust);
        EventCenter.GetInstance().AddEventListener("Íæ¼Ò³å´Ì½áÊø", PlayerRushOver);
    }

    public void PlayerRushDust(int faceDir)
    {
        if (!particle.isPlaying)
        {
            particle.Play();
            if (faceDir<0) //ÓÒ
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
