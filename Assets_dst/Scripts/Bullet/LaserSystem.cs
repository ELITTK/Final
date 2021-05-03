using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject Inner, Outer;
    public float startTime, durationTime, fadeTime;

    private int currentState = 1;
    private float timer;
    private MeshRenderer innerMesh, outerMesh;
    private Material innerMat, outerMat;
    private Color innerColor, outerColor;
    private CapsuleCollider capCollider;
    private void Awake()
    {
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
            float factor = Mathf.Pow(2, timer / startTime);
            innerMat.SetColor("Color", new Color(innerColor.r * factor, innerColor.g * factor, innerColor.b * factor));
        }
        else if (timer < durationTime + startTime)
        {
            if (currentState == 1)
            {
                capCollider.enabled = true;
                currentState = 2;
            }
            innerMat.SetColor("Color", innerColor);
        }
        else if (timer < fadeTime + durationTime + startTime)
        {
            if (currentState == 2)
            {
                capCollider.enabled = false;
                currentState = 3;
            }
            float factor = Mathf.Pow(2, (timer - durationTime - startTime) / fadeTime);
            innerMat.SetColor("Color", new Color(innerColor.r * factor, innerColor.g * factor, innerColor.b * factor));
        }
        else
        {

        }
    }

    public float GetTime()
    {
        return startTime + durationTime + fadeTime;
    }
}
