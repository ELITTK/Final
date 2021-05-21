using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSystem : MonoBehaviour
{
    public GameObject Inner, Outer;
    public float startTime, durationTime, fadeTime;
    public int damage = 10;

    private int currentState = 1;
    private float timer;
    private MeshRenderer innerMesh, outerMesh;
    private Material innerMat, outerMat;
    private Color innerColor, outerColor;
    private CapsuleCollider capCollider;
    private void Start()
    {
        Debug.Log("start");
        innerMesh = Inner.GetComponent<MeshRenderer>();
        outerMesh = Outer.GetComponent<MeshRenderer>();
        innerMat = innerMesh.material;
        outerMat = outerMesh.material;
        innerColor = innerMat.color;
        outerColor = outerMat.color;
        capCollider = Inner.GetComponent<CapsuleCollider>();
    }
    private void FixedUpdate()
    {
        
        Blink();
    }

    void Blink()
    {
        timer += Time.deltaTime;
        
        if (timer < startTime)
        {
            float factor = Mathf.Pow(2, timer / startTime) - 1f;
            
            innerMat.SetColor("_Color", new Color(innerColor.r * factor, innerColor.g * factor, innerColor.b * factor));
        }
        else if (timer < durationTime + startTime)
        {
            if (currentState == 1)
            {
                capCollider.enabled = true;
                currentState = 2;
            }
            innerMat.SetColor("_Color", innerColor);
        }
        else if (timer < fadeTime + durationTime + startTime)
        {
            if (currentState == 2)
            {
                capCollider.enabled = false;
                currentState = 3;
            }
            float factor = Mathf.Pow(2, (timer - durationTime - startTime) / fadeTime) - 1;
            Debug.Log(factor);
            innerMat.SetColor("_Color", new Color(innerColor.r * factor, innerColor.g * factor, innerColor.b * factor));
        }
        else
        {

        }
    }

    public float GetTime()
    {
        return startTime + durationTime + fadeTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("111");
            other.gameObject.GetComponent<PlayerMovement>().Damage(damage);
        }
    }
}
