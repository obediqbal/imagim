using DKH.SkillSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/Attack Buff")]
public class ATKBUFF : SkillBase
{
    public override Type SkillData => typeof(DemoAtkBuffData);

    public override bool ExecuteAs(ISkillable executor)
    {
        DemoAtkBuffData data = (DemoAtkBuffData)executor.GetSkillData(this);
        if (data.Cooldown > 0)
        {
            return false;
        }
        data.buff.ATKBUFF();
        data.Cooldown = Cooldown;
        Debug.Log(executor + " casted skill");
        return true;
    }
}

/// <summary>
/// Handles skill unique data for each skill such as cooldown or skills with "deals damage based on the amount of kills you have"
/// </summary>
public class DemoAtkBuffData : SkillData, ISkillDataCooldown
{
    public float Cooldown { get; set; }
    public BUFF buff;
}