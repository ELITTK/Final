using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneShift : MonoBehaviour
{
    public void Shift()
    {
        SceneManager.LoadScene("Level2");
    }
 
}
