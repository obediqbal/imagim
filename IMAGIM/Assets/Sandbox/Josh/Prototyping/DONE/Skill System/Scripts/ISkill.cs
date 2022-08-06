using System;

namespace DKH.SkillSystem
{
    public interface ISkill
    {
        public abstract Type SkillData { get; }
        public abstract void ExecuteAs(ISkillable executor);
    }
}