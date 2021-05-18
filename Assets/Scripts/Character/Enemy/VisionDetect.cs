using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionDetect : MonoBehaviour
{
    private bool isFound = false;
    private Transform tempTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isFound = true;
            tempTransform = other.transform;
        }
    }
    public Transform getDetected()
    {
        return tempTransform;
    }

    public bool isFoundTarget()
    {
        if (isFound)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
