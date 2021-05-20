using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDust : MonoBehaviour
{
    private ParticleSystem particle;

    public void Start()
    {
        particle = GetComponent<ParticleSystem>();
        EventCenter.GetInstance().AddEventListener("�����Ծ", PlayJumpDust);
    }

    public void PlayJumpDust()
    {
        if (!particle.isPlaying)
        {
            particle.Play();
        }
    }


}
