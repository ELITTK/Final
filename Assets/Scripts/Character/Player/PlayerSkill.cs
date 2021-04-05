using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public SkillData skill0;
    /*
    public SkillData skill1;
    public SkillData skillQ;
    public SkillData skillW;
    public SkillData skillE;
    public SkillData skillR;
    */

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            skill0.UseSkill(this.transform);
        }
    }

}
