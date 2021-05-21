using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDroneAtkRange : MonoBehaviour
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

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isFound = false;
            //tempTransform = null;
        }
    }

    public Transform getDetected()
    {
        return tempTransform;
    }

    public bool isFoundTarget()
    {
        return isFound;
    }
}
