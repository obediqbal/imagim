using DKH.SkillSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DKH.Debugging
{
    /// <summary>
    /// In this script the object does not contain the skill references, the UI calls the object to cast the skill
    /// the object does not actually "own" the skills
    /// </summary>
    public class DemoSkilledObject : MonoBehaviour, ISkillable
    {
        [SerializeField] TMP_Text cooldownTimer;

        /// <summary>
        /// The skillable object needs a data container for each skill
        /// </summary>
        Dictionary<SkillBase, SkillData> skillData = new Dictionary<SkillBase, SkillData>();

        /// <summary>
        /// Default ExecuteSkill method calls skill execution on the skill, can have conditionals to check if the character is stunned or etc.
        /// </summary>
        public void ExecuteSkill(SkillBase skill)
        {
            skill.ExecuteAs(this);
        }

        /// <summary>
        /// Default GetSkillData, tries to get the value and creates a new SkillData of the skill if not found
        /// </summary>
        public SkillData GetSkillData(SkillBase skill)
        {
            SkillData data;
            if (!skillData.TryGetValue(skill, out data))
            {
                // Basically
                // data = new DemoDebugLogSkillData()
                data = Activator.CreateInstance(skill.SkillData) as SkillData;
                skillData.Add(skill, data);
            }
            return data;
        }

        private void Update()
        {
            float skillCooldown = 0;
            foreach (var skillData in skillData.Values)
            {
                ISkillDataCooldown cooldownSkill = (ISkillDataCooldown)skillData;
                cooldownSkill.Cooldown -= Time.deltaTime;
                skillCooldown = cooldownSkill.Cooldown;
            }
            cooldownTimer.text = (skillCooldown <= 0) ? "Ready" : $"{Mathf.Round(skillCooldown * 10f) / 10f}s";
        }
    }
}