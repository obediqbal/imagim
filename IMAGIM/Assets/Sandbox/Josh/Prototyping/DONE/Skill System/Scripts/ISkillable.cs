using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH.SkillSystem
{
    /// <summary>
    /// Represents an object that can use skills
    /// </summary>
    public interface ISkillable
    {
        void ExecuteSkill(SkillBase skill);
        SkillData GetSkillData(SkillBase skill);
    }
}