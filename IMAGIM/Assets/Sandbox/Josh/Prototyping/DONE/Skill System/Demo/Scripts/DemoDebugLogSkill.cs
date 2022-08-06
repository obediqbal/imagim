using DKH.SkillSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH.Debugging
{
    [CreateAssetMenu(fileName = "DemoDebugLogSkill", menuName = "SO/Skills/DemoDebugLog")]
    public class DemoDebugLogSkill : SkillBase
    {
        public override Type SkillData => typeof(DemoDebugLogSkillData);

        public override bool ExecuteAs(ISkillable executor)
        {
            DemoDebugLogSkillData data = (DemoDebugLogSkillData)executor.GetSkillData(this);
            if (data.Cooldown > 0)
            {
                Debug.Log($"{executor} failed to cast skill ({Mathf.Round(data.Cooldown * 10f) / 10f})s remaining");
                return false;
            }
            data.Cooldown = Cooldown;
            Debug.Log(executor + " casted skill");
            return true;
        }
    }

    /// <summary>
    /// Handles skill unique data for each skill such as cooldown or skills with "deals damage based on the amount of kills you have"
    /// </summary>
    public class DemoDebugLogSkillData : SkillData, ISkillDataCooldown
    {
        public float Cooldown { get; set; }
    }
}