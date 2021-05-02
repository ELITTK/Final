using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public List<GameObject> skillCasters;

    public List<float> skillCurrentCD;

    public List<KeyCode> skillKeyCode;

    private void Update()
    {
        for (int i = 0; i < skillCasters.Count; i++)
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
            BaseSkillCaster casterScript = skillCasters[i].GetComponent<BaseSkillCaster>();
            casterScript.ExcuteSkill();
            skillCurrentCD[i] = casterScript.skillData.maxCd;
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
