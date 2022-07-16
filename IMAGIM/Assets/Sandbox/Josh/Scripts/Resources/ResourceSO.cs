using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sandbox.Josh.Scripts
{
    /// <summary>
    /// Represents a resource with displayname and description for UI that has a value.
    /// </summary>
    [CreateAssetMenu(fileName = "Resource", menuName = "ScriptableObjects/Resource")]
    public class ResourceSO : ScriptableObject
    {
        public string DisplayName;
        public string Description;
        [SerializeField]
        private float DefaultValue;
        protected float _value;
        public virtual void SetDefaultValue()
        {
            _value = DefaultValue;
        }
        public virtual float GetValue()
        {
            return _value;
        }
        public virtual void SetValue(float value)
        {
            _value = value;
        }
        public virtual void AddValue(float value)
        {
            _value += value;
        }
    }
}