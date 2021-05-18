using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sk2_PlayerLaser : BaseLaser
{
    public KeyCode ExecuteKey;

    protected override bool SetIsShootingTrue()
    {
        return Input.GetKeyDown(ExecuteKey);
    }

    protected override bool SetIsShootingFalse()
    {
        return Input.GetKeyUp(ExecuteKey);
    }
}
