using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public SkillData skill0;
    public SkillData skillSpecial;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            skill0.UseSkill(this.transform);
        }

        if (Input.GetKeyDown("f"))
        {
            skillSpecial.UseSkill(this.transform);
        }
    }

}
