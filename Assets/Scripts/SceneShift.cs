using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneShift : MonoBehaviour
{
    public AudioSource music;
    //private AudioClip meat;
    private void Start()
    {
        music = gameObject.GetComponent<AudioSource>();
        //meat = Resources.Load<AudioClip>("music/meat");
    }
    public void Shift()
    {
        SceneManager.LoadScene("StartScene1");
    }
 
    public void Sound()
    {
        music.Play();
    }
}
