using System;
using UnityEngine;

namespace DKH.SkillSystem
{
    /// <summary>
    /// Represents a generic skill to be inherited in class and later instanstiated as ScriptableObject.
    /// </summary>
    public abstract class SkillBase : ScriptableObject, ISkill
    {
        public string DisplayName;
        public string Description;
        public float Cooldown;
        public abstract Type SkillData { get; }
        public abstract void ExecuteAs(ISkillable executor);
    }
}