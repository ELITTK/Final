using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    public float bulletDmg;

    private void Start()
    {

        var main = GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //碰撞事件
        if (!collision.gameObject.CompareTag("Player"))
        {
            StopAllParticleSystem();
        }
    }

    private void StopAllParticleSystem()
    {
        //不让粒子系统再生成新粒子
        ParticleSystem[] particleSystems;
         particleSystems = GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Stop();
        }
    }

    private void OnParticleSystemStopped()
    {
        //当该物体已经没有存活的粒子时，隐藏物体以便让对象池再次调用
        gameObject.SetActive(false);
        Debug.Log("所有粒子已死亡");
    }
}
