using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH.ResourceSystem
{
    /// <summary>
    /// Represents a resource with displayname and description for UI that has a value.
    /// </summary>
    [CreateAssetMenu(fileName = "Resource", menuName = "SO/Resource")]
    public class Resource : ScriptableObject
    {
        public string DisplayName;
        public string Description;
        [SerializeField]
        private float DefaultValue;
        [SerializeField]
        [ReadOnly]
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