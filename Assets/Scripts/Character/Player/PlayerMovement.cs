using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalMove, verticalMove;
    private InputMgr InputMgr;
    // Start is called before the first frame update
    void Start()
    {
        InputMgr = new InputMgr();
        InputMgr.StartOrEndCheck(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GroundMove()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
    }
}
