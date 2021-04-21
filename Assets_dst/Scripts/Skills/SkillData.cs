using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/New Skill")]
public class SkillData : ScriptableObject
{
    public int skillID;
    public Sprite skillSpirte;//技能图片

    public string skillName;
    [TextArea]
    public string skillDes;//技能描述

    public GameObject skillCaster;//技能释放器

    public float maxCd = 1;


    public void UseSkill(Transform ownerTrans)
    {
        //给技能释放者生成一个指定的技能释放器，让它来触发具体技能效果。
        GameObject tempCaster = Instantiate(skillCaster, ownerTrans);
        Destroy(tempCaster, 1);
    }
}
