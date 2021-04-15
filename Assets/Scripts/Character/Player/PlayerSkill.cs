using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public List<SkillData> skills;

    public List<float> skillCurrentCD;

    public List<KeyCode> skillKeyCode;

    //public List<GameObject> skillCaster;

    private void Update()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            if (Input.GetKey(skillKeyCode[i]))
            {
                TouchSkillKey(i);
            }
        }

        FreshAllSkill();
    }

    private void TouchSkillKey(int i)
    {
        if (skillCurrentCD[i] <= 0)
        {
            skills[i].UseSkill(transform);
            skillCurrentCD[i] = skills[i].maxCd;
        }
    }

    private void FreshAllSkill()
    {
        for (int i = 0; i < skillCurrentCD.Count; i++)
        {
            if (skillCurrentCD[i] > 0)
            {
                skillCurrentCD[i] -= Time.deltaTime;
            }
        }
    }

}
