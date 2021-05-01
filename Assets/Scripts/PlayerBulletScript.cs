using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    public float bulletDmg;

    private float lifeTime = 1f;//一旦子弹存活超过这个时间，强制让子弹失效
    private bool isHidden = false;//子弹是否已经执行了HideBullet，目前只参与子弹计时失效

    private void Start()
    {
        var main = GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    private void OnEnable()
    {
        Invoke("StopAllParticleSystem", lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //碰撞事件
        if (!collision.gameObject.CompareTag("Player"))
        {
            StopAllParticleSystem();
            Enemy enemyScript = collision.gameObject.GetComponent<Enemy>();
            if (enemyScript)
            {
                enemyScript.takeDmg(bulletDmg);
            }
        }
    }

    private void StopAllParticleSystem()
    {
        CancelInvoke();
        if (!isHidden)
        {
            //隐藏子弹球体
            HideBullet();
            //停止粒子系统，不让粒子系统再生成新粒子
            ParticleSystem[] particleSystems;
            particleSystems = GetComponentsInChildren<ParticleSystem>();

            foreach (ParticleSystem ps in particleSystems)
            {
                ps.Stop();
            }
        }
    }

    private void OnParticleSystemStopped()
    {
        //当该物体已经没有存活的粒子时，隐藏物体以便让对象池再次调用
        //Debug:粒子在屏幕外不更新不渲染不触发这个函数，据说是元老级bug，要勾上粒子系统的submitter才能解决
        UnHideBullet();//解除子弹球体的隐藏
        gameObject.SetActive(false);
    }

    private void HideBullet()
    {
        isHidden = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        if (GetComponent<Collider>())
        {
            GetComponent<Collider>().enabled = false;
        }
    }
    private void UnHideBullet()
    {
        isHidden = false;
        GetComponent<MeshRenderer>().enabled = true;
        if (GetComponent<Collider>())
        {
            GetComponent<Collider>().enabled = true;
        }
    }
}
