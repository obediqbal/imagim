using System;

namespace DKH.SkillSystem
{
    /// <summary>
    /// Marks a class as a skill
    /// </summary>
    public interface ISkill
    {
        public abstract Type SkillData { get; }
        public abstract bool ExecuteAs(ISkillable executor);
    }
}