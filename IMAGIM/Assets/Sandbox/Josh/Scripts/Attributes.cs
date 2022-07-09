using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sandbox.Josh.Scripts
{
    /// <summary>
    /// Represents attribute values that can be modified for an entity.
    /// </summary>
    public class Attributes
    {
        public enum Type
        {
            GenericMaxHealth,
            PlayerMaxMana,
            IgnoreThis,
        }
        public float[] values;
        public Attributes()
        {
            values = new float[(int)Type.IgnoreThis];
        }
        public void SetAttributeValue(Type type, float value)
        {
            values[(int)type] = value;
        }
        public void AddAttributeValue(Type type, float value)
        {
            values[(int)type] += value;
        }
        public float GetAttributeValue(Type type)
        {
            return values[(int)type];
        }
    }
}