using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_controller : MonoBehaviour
{
    public GameObject player;
    private float max = -195;
    private float preposx;
    private bool isWin = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        preposx = player.transform.position.x;
        if (isWin)
        {
            inCorridor();
        }
        else
        {
            Corridor_front();
        }
    }

    void Corridor_front()
    {
        if (preposx >= max)
        {
            transform.position = new Vector3(player.transform.position.x - 0.5f, player.transform.position.y + 2f, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x - 0.5f, player.transform.position.y + 2f, transform.position.z);
        }
    }

    void inCorridor()
    {
        transform.position = new Vector3(player.transform.position.x - 0.5f, player.transform.position.y + 2f, 0);
    }
}
