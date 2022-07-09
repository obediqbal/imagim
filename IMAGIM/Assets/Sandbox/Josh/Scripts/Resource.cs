using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sandbox.Josh.Scripts
{
    /// <summary>
    /// Represents a resource with displayname and description for UI that has a value.
    /// </summary>
    public abstract class Resource
    {
        public abstract string DisplayName { get; }
        public abstract string Description { get; }
        protected int value;
        public virtual int GetValue()
        {
            return value;
        }
        public virtual void SetValue(int value)
        {
            this.value = value;
        }
        public virtual void AddValue(int value)
        {
            this.value += value;
        }
    }
}