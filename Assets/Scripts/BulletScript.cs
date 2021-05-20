using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理子弹的碰撞：伤害，粒子，对象池
/// </summary>
public class BulletScript : MonoBehaviour
{
    public float bulletDmg;

    private float lifeTime = 3f;//一旦子弹存活超过这个时间，强制让子弹失效
    private bool isHidden = false;//子弹是否已经执行了HideBullet，目前只参与子弹计时失效
    public bool isPlayerBullet;//是否是玩家射出的子弹

    [Header("击中时的视觉效果")]
    public bool isPlayHitPS = false;//命中时是否开启特定的粒子系统
    public GameObject psNeed2Open;

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
        if (isPlayerBullet)
        {
            //碰撞事件
            if (!collision.gameObject.CompareTag("Player"))
            {
                Ready2DisableBullet();
                Enemy enemyScript = collision.gameObject.GetComponent<Enemy>();
                if (enemyScript)
                {
                    enemyScript.takeDmg(bulletDmg);
                }
            }
        }
        else
        {
            //碰撞事件
            if (collision.gameObject.CompareTag("Player"))
            {
                Ready2DisableBullet();
                //对玩家造成伤害
                PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
                if (playerMovement)
                {
                    playerMovement.TakeDmg(bulletDmg);
                }
            }
        }
    }

    //准备禁用子弹
    private void Ready2DisableBullet()
    {
        CancelInvoke();
        if (!isPlayHitPS)
        {
            StopAllParticleSystem();
        }
        else
        {
            psNeed2Open.SetActive(true);
            StopAllParticleSystem();
        }
    }

    //关闭粒子系统，为禁用子弹做准备
    private void StopAllParticleSystem()
    {
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

            if (isPlayHitPS)
            {
                psNeed2Open.GetComponent<ParticleSystem>().Play();
            }
        }
    }

    private void OnParticleSystemStopped()
    {
        //当该物体已经没有存活的粒子时，隐藏物体以便让对象池再次调用
        //Debug:粒子在屏幕外不更新不渲染不触发这个函数，据说是元老级bug，要勾上粒子系统的submitter才能解决
        UnHideBullet();//解除子弹球体的隐藏

        if (isPlayHitPS)
        {
            psNeed2Open.SetActive(false);
        }

        
        if (gameObject.transform.parent)
        {
            transform.parent.gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void HideBullet()
    {
        isHidden = true;
        if (GetComponent<MeshRenderer>())
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        if (GetComponent<Collider>())
        {
            GetComponent<Collider>().enabled = false;
        }
        GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        //GetComponent<Rigidbody>().useGravity = false;
    }
    private void UnHideBullet()
    {
        isHidden = false;
        if (GetComponent<MeshRenderer>())
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
        if (GetComponent<Collider>())
        {
            GetComponent<Collider>().enabled = true;
        }
        //GetComponent<Rigidbody>().useGravity = true;
    }
}
