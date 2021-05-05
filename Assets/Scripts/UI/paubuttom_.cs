using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class paubuttom_ : MonoBehaviour
{
    public GameObject controller;
    public void Click()
    {
        Time.timeScale = 0;
        controller.SetActive(true);
    }
}
