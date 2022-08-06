using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH.SkillSystem
{
    /// <summary>
    /// SkillData interface to implement cooldown
    /// </summary>
    public interface ISkillDataCooldown
    {
        float Cooldown { get; set; }
    }
}